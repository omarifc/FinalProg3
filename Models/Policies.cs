using Microsoft.AspNetCore.Authorization;


namespace ProyectoPrueba.Models
{
    public class Policies
    {
        public const string Admin = "admin";
        public const string User = "user";

        public static AuthorizationPolicy adminPolicy(){
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy userPolicy(){
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
    }
}