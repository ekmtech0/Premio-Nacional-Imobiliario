using System;

namespace server.Helpers;

public static class HashHelper
{
    public static string Hash(string str) =>
        BCrypt.Net.BCrypt.HashPassword(str);
    
    public static bool VerifyHash(string str, string hash)=>
        BCrypt.Net.BCrypt.Verify(str, hash);
}
