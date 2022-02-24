﻿namespace RYB.Model.AppSettings;

public class Jwt
{
    public string Secret { get; set; }

    // refresh token time to live (in days), inactive tokens are
    // automatically deleted from the database after this time
    public int RefreshTokenTTL { get; set; }
}

