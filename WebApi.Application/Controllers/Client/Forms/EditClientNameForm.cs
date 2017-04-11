﻿namespace WebApi.Application.Controllers.Client.Forms
{
    using Infrastructure.Forms;

    public class EditClientNameForm : IApiForm
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}