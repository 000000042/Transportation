﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.DTOs.AdminDTO;
using Transportation.Core.DTOs.ContractDTO;
using Transportation.DataLayer.Entities.Contract;

namespace Transportation.Core.Services
{
    public interface IContractService
    {
        bool IsUserDriver(string email);

        bool IsUserContractor(string email);

        List<ShowContractsViewModel> GetContractsForShow();

        List<SelectListItem> GetDriversAsItems();

        List<SelectListItem> GetContractorsAsItems();

        int SignContract(int requestId);

        ContractSign GetContractToShow(int contractId);

        List<SelectListItem> GetTruckTypesToSelect();

        int AddCargoAnnounce(CargoAnnounceViewModel announce);

        CargoAnnounce GetCargoAnnounceById(int announceId);

        List<AnnounceViewModel> GetAnnouncesToShow();

        void AddRequestToAnnounce(RequestViewModel request);

        List<CargoRequest> GetRequestsForAnnounce(int announceId);

        bool DeclineRequest(int requestId);

        bool IsExistContractAnnounce(int announceId);

        bool IsExistAnnounce(int announceId);

        bool IsExistRequests(int announceId);
    }
}
