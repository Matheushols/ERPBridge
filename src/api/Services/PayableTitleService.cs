using api.Models;
using ERPBridge.Class;
using ERPBridge.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ERPBridge.Services
{
    public class PayableTitleService
    {
        private readonly AppDbContext _dbContext;

        public PayableTitleService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(PayableTitle? payableTitle, string? errorMessage)> CreatePayableTitleAsync(CreatePayableTitleDto dto)
        {
            try
            {
                var payableTitle = new PayableTitle
                {
                    originId = dto.OriginId,
                    userId = dto.UserId,
                    systemName = dto.SystemName,
                    externalId = dto.ExternalId,
                    externalRetry = dto.ExternalRetry,
                    errorExternalId = dto.ErrorExternalId,
                    creationDate = dto.CreationDate,
                    statusExternal = dto.StatusExternal,
                    titleInformations = dto.TitleInformations
                };

                _dbContext.Add(payableTitle);
                await _dbContext.SaveChangesAsync();

                return (payableTitle, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<List<PayableTitle>> GetAllPayableTitlesAsync()
        {
            return await _dbContext.PayableTitles.ToListAsync();
        }

        public async Task<PayableTitle?> GetPayableTitleByIdAsync(int id)
        {
            return await _dbContext.PayableTitles.FindAsync(id);
        }

        public async Task<(PayableTitle? payableTitle, string? errorMessage)> UpdatePayableTitleAsync(int id, UpdatePayableTitleDto dto)
        {
            try
            {
                var payableTitle = await _dbContext.PayableTitles.FindAsync(id);

                if (payableTitle == null)
                {
                    return (null, null);
                }

                // Atualiza apenas as propriedades não nulas
                if (dto.OriginId != null) payableTitle.originId = dto.OriginId;
                if (dto.UserId != null) payableTitle.userId = dto.UserId.Value;
                if (dto.SystemName != null) payableTitle.systemName = dto.SystemName;
                if (dto.ExternalId != null) payableTitle.externalId = dto.ExternalId;
                if (dto.ExternalRetry != null) payableTitle.externalRetry = dto.ExternalRetry.Value;
                if (dto.ErrorExternalId != null) payableTitle.errorExternalId = dto.ErrorExternalId;
                if (dto.CreationDate != null) payableTitle.creationDate = dto.CreationDate.Value;
                if (dto.StatusExternal != null) payableTitle.statusExternal = dto.StatusExternal.Value;
                if (dto.TitleInformations != null) payableTitle.titleInformations = dto.TitleInformations;

                _dbContext.Update(payableTitle);
                await _dbContext.SaveChangesAsync();

                return (payableTitle, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<bool> DeletePayableTitleAsync(int id)
        {
            var payableTitle = await _dbContext.PayableTitles.FindAsync(id);

            if (payableTitle == null)
            {
                return false;
            }

            _dbContext.PayableTitles.Remove(payableTitle);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}