using System;

namespace server.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = null!;
    public DateTime Expires { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? Revoked { get; set; }

    public Guid AdmId { get; set; }
    public Adm Adm { get; set; } = null!;

    public bool IsActive => Revoked == null && !IsExpired;
    public bool IsExpired => DateTime.UtcNow >= Expires;

    public bool IsRevoked => Revoked != null;

}
