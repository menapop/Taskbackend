using System;
using System.Collections.Generic;
using System.Text;
using DTOS.Dto;

namespace Service.Helpers.Email
{
    public interface IEmailServie
    {
        void SendEmail(EmailDto email);

    }
}
