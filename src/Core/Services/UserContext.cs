﻿namespace Core.Services
{
    public class UserContext : IUserContext
    {
        public ulong TenantId { get; set; }
    }
}