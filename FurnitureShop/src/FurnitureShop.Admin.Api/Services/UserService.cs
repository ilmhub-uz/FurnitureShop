﻿using Flurl.Util;
using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FurnitureShop.Admin.Api.Services
{
    [Scoped]
    public class UserService : IUserService
    {
        private readonly IUnitOfWork? unitOfWork;

        public UserService(IUnitOfWork? unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task DeleteUser(Guid userId)
        {
          var user = unitOfWork.AppUsers.GetById(userId);
            if (user is null)
                throw new NotFoundException<AppUser>();
            user.Status = EUserStatus.Deleted;
          await unitOfWork.AppUsers.Update(user);
        }

        public async Task<UserView> GetUserById(Guid userId)
        {
            var userview = unitOfWork.AppUsers.Find(user => user.Id == userId).Adapt<UserView>();
            if (userview is null)
                throw new NotFoundException<AppUser>();
            return userview;
        }

        public async Task<List<UserView>> GetUsers(UserFilterDto userFilterDto)
        {
            var query =  unitOfWork.AppUsers.GetAll();
            //select from contracts;

            query = userFilterDto.UserStatus switch
                {
                    EUserStatus.Created => query.Where(user => user.Status == EUserStatus.Created),
                    EUserStatus.Deleted => query.Where(user => user.Status == EUserStatus.Deleted),
                    EUserStatus.Active => query.Where(user => user.Status == EUserStatus.Active),
                    EUserStatus.InActive => query.Where(user => user.Status == EUserStatus.InActive),
                    _ => query,
                };
            
            var data =  await query.ToPagedListAsync(userFilterDto);
            if(data is null) new List<UserView>();

            return data.Adapt<List<UserView>>();
        }

        [HttpPut]
        public async Task UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            var user = unitOfWork.AppUsers.GetById(userId);
            if(user is null) throw new NotFoundException<AppUser>();
            user.UserName = updateUserDto.UserName;
            user.Status = updateUserDto.UserStatus;
            await unitOfWork.AppUsers.Update(user);
        }
    }
}
