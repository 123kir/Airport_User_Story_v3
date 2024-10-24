﻿using DataGrid.Framework.ApplicantManager.Models;
using DataGrid.Framework.Contracts;
using DataGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGrid.Framework.Contracts;
using DataGrid.Framework.Contracts.Models;


namespace DataGrid.Framework.ApplicantManager
{
    /// <inheritdoc cref="IApplicantManager"/>
    public class ApplicantManager : IApplicantManager
    {
        private IApplicantStorage applicantStorage;
        /// <summary>
        /// Конструктор
        /// </summary
        public ApplicantManager(IApplicantStorage applicantStorage)
        {
            this.applicantStorage = applicantStorage;
        }
        /// <inheritdoc cref="IApplicantManager.AddAsync(Applicant)"/>
        public async Task<Applicant> AddAsync(Applicant applicant)
        {
            var result = await applicantStorage.AddAsync(applicant);
            return result;
        }
        /// <inheritdoc cref="IApplicantManager.DeleteAsync(Guid)"/>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await applicantStorage.DeleteAsync(id);
            return result;
        }
        /// <inheritdoc cref="IApplicantManager.EditAsync(Applicant)"/>
        public Task EditAsync(Applicant applicant)
            => applicantStorage.EditAsync(applicant);

        /// <inheritdoc cref="IApplicantManager.GetAllAsync()"/>
        public Task<IReadOnlyCollection<Applicant>> GetAllAsync()
            => applicantStorage.GetAllAsync();

        /// <inheritdoc cref="IApplicantManager.GetAllAsync()"/>
        public async Task<IApplicantStats> GetStatsAsync()
        {
            var result = await applicantStorage.GetAllAsync();
            return new ApplicantStatsModel
            {
                Count = result.Count,
                MaleCount = result.Where(x => x.Gender == Gender.Male).Count(),
                FemaleCount = result.Where(x => x.Gender == Gender.Female).Count(),
                FullTimeCount = result.Where(x => x.Education == Education.FullTime).Count(),
                FTPTCount = result.Where(x => x.Education == Education.FTPT).Count(),
                СorrespondenceCount = result.Where(x => x.Education == Education.Сorrespondence).Count(),
                TotalScoreCount = result.Where(x => x.TotalScore >= 150).Count(),
            };
        }
    }
}
