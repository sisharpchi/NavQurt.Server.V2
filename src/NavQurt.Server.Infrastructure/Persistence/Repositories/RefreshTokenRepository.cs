﻿using Microsoft.EntityFrameworkCore;
using NavQurt.Server.Application.Interfaces;
using NavQurt.Server.Core.Errors;
using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository(AppDbContext _context) : IRefreshTokenRepository
{
    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRefreshToken(string refreshToken)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
        if (token == null)
        {
            throw new EntityNotFoundException();
        }
        _context.RefreshTokens.Remove(token);
    }

    public async Task<RefreshToken> SelectRefreshToken(string refreshToken, long userId) => await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken && rt.UserId == userId);
}
