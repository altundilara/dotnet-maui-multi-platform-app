using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using _23010310007_DilaraAltun_FinalOdev.VeriModelleri;

namespace _23010310007_DilaraAltun_FinalOdev.services
{
    internal static class FirebaseServices
    {
        private const string ApiKey = "AIzaSyBkulmLf-TRHPpIpqGLhykO_ww6tq7tJqY";
        private const string DbUrl = "https://final-odev-134ab-default-rtdb.firebaseio.com/";

        private static FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        private static FirebaseClient dbClient = new FirebaseClient(DbUrl);

        
        public static async Task<bool> Register(string email, string password)
        {
            try
            {
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                return auth != null;
            }
            catch { return false; }
        }

        public static async Task<bool> Login(string email, string password)
        {
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                return auth != null;
            }
            catch { return false; }
        }

    }
}