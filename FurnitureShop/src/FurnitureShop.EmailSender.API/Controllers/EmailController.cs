﻿using FurnitureShop.EmailSender.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.EmailSender.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SendEmailController : ControllerBase
{
    private readonly IEmailSender _emailSender;

    public SendEmailController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost]
    public IActionResult Get([FromQuery] string subject, [FromQuery] string to, [FromQuery] string mailBody)
    {
        var message = new Message(new string[] { $"{to}" }, subject, mailBody);
        _emailSender.SendEmail(message);
        return Ok($@"Email sent successfully.
    To: {to};
    Subject: {subject};
    Message: {mailBody};");
    }
}