namespace MyWallWebAPI.Infrastructure.Config.Identity
{
    public static class CustomClaimTypes
    {
        public const string ClientId = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/clientId";
        public const string AllowedOrigins = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/allowedorigins";
        public const string UserLogin = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/userid";
        public const string SecurityStamp = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/securityStamp";

        public const string UsuarioId = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/usuarioid";
        public const string Email = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/email";
        public const string PessoaId = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/pessoaid";
        public const string PessoaDocumento = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/pessoadocumento";
        public const string TenantId = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/tenantid";
        public const string Hierarchy = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/hierarchy";
        public const string Permission = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/permission";
        public const string PermissionBlock = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/permission.block";
        public const string ForcePasswordChange = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/force.password.change";
    }
}
