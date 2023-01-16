﻿namespace FurnitureShop.Data.Entities;
public enum EPermission
{
    None,
    CanCreateUser,
    CanReadUser,
    CanUpdateUser,
    CanDeleteUser,
    CanCreateCart,  
    CanReadCart,   
    CanDeleteFromCart,
    CanCreateOrganization,
    CanReadOrganization, 
    CanUpdateOrganization, 
    CanDeleteOrganization, 
    CanCreateContract,
    CanReadContract, 
    CanUpdateContract,
    CanDeleteContract,
    CanCreateEmployee,
    CanReadEmployee, 
    CanUpdateEmployee, 
    CanDeleteEmployee, 
    CanCreateProduct,
    CanReadProduct, 
    CanUpdateProduct,
    CanDeleteProduct,
    CanReadProfile, 
    CanUpdateProfile, 

    // add more permissions as needed
}