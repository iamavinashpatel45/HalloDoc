using DocumentFormat.OpenXml.Office2016.Presentation.Command;
using Microsoft.AspNetCore.Http;
using Repositories.DataModels;
using Repositories.Interfaces;
using Services.Interfaces.AdminServices;
using Services.Interfaces.AuthServices;
using Services.ViewModels.Admin;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace Services.Implementation.AdminServices
{
    public class ViewNotesService : IViewNotesService
    {
        private readonly IRequestNotesRepository _requestNotesRepository;
        private readonly IRequestStatusLogRepository _requestSatatusLogRepository;
        private readonly IRequestClientRepository _requestClientRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IJwtService _jwtService;

        public ViewNotesService(IRequestNotesRepository requestNotesRepository, IRequestStatusLogRepository requestSatatusLogRepository, 
                                      IRequestClientRepository requestClientRepository, IRequestRepository requestRepository, 
                                      IJwtService jwtService)
        {
            _requestNotesRepository = requestNotesRepository;
            _requestSatatusLogRepository = requestSatatusLogRepository;
            _requestRepository = requestRepository;
            _requestClientRepository = requestClientRepository;
            _jwtService = jwtService;
        }
        public ViewNotes GetNotes(int RequestId)
        {
            RequestNote requestNote = _requestNotesRepository.GetRequestNoteByRequestId(RequestId);
            return new ViewNotes()
            {
                RequestId = RequestId,
                AdminNotes = requestNote!=null?requestNote.AdminNotes:null,
                PhysicianNotes = requestNote != null ? requestNote.PhysicianNotes : null,
                TransferNotes = _requestSatatusLogRepository.GetRequestStatusLogByRequestId(RequestId)
                                    .ToDictionary(requestStatusLog => requestStatusLog.CreatedDate.ToString("MMM dd,yyy"),
                                                  requestStatusLog => requestStatusLog.Notes != null ? requestStatusLog.Notes : ""),
            };
        }

        public async Task<bool> addAdminNotes(String newNotes, int requestId, int aspNetUserId, bool isAdmin)
        {
            RequestNote requestNote = _requestNotesRepository.GetRequestNoteByRequestId(requestId);
            if(requestNote == null)
            {
                RequestNote _requestNote = new()
                {
                    RequestId = requestId,
                    AdminNotes = isAdmin ? newNotes : null,
                    PhysicianNotes = isAdmin ? null : newNotes,
                    CreatedDate = DateTime.Now,
                };
                return await _requestNotesRepository.addRequestNote(_requestNote);
            }
            requestNote.AdminNotes = isAdmin ? newNotes : null;
            requestNote.PhysicianNotes = isAdmin ? null : newNotes;
            requestNote.ModifiedDate = DateTime.Now;
            requestNote.ModifiedBy = aspNetUserId;
            return await _requestNotesRepository.updateRequestNote(requestNote);
        }

        public async Task<bool> cancleRequest(CancelPopUp model)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(model.RequestId);
            requestClient.Status = 3;
            if(await _requestClientRepository.updateRequestClient(requestClient))
            {
                Request request = _requestRepository.getRequestByRequestId(model.RequestId);
                request.CaseTagId = model.Reason;
                request.ModifiedDate = DateTime.Now;
                if(await _requestRepository.updateRequest(request))
                {
                    return await _requestSatatusLogRepository.addRequestSatatusLog(
                        new RequestStatusLog()
                        {
                            RequestId = model.RequestId,
                            Status = 3,
                            CreatedDate = DateTime.Now,
                            Notes = model.AdminTransferNotes,
                        });
                }
            }
            return false;
        }

        public async Task<bool> agreementDeclined(Agreement model)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(model.RequestId);
            requestClient.Status = 8;
            if(await _requestClientRepository.updateRequestClient(requestClient))
            {
                return await _requestSatatusLogRepository.addRequestSatatusLog(
                    new RequestStatusLog()
                    {
                        RequestId = model.RequestId,
                        Status = 8,
                        CreatedDate = DateTime.Now,
                        Notes = model.CancelationReson,
                    });
            }
            return false;
        }

        public async Task<bool> agreementAgree(Agreement model)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(model.RequestId);
            requestClient.Status = 5;
            if(await _requestClientRepository.updateRequestClient(requestClient))
            {
                Request request = _requestRepository.getRequestByRequestId(model.RequestId);
                request.AcceptedDate = DateTime.Now;
                request.ModifiedDate = DateTime.Now;
                return await _requestRepository.updateRequest(request);
            }
            return false;
        }

        public async Task<bool> assignRequest(AssignAndTransferPopUp model)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(model.RequestId);
            requestClient.PhysicianId = model.SelectedPhysician;
            if(await _requestClientRepository.updateRequestClient(requestClient))
            {
                return await _requestSatatusLogRepository.addRequestSatatusLog(
                    new RequestStatusLog()
                    {
                        RequestId = model.RequestId,
                        Status = 1,
                        CreatedDate = DateTime.Now,
                        Notes = model.AdminTransferNotes,
                        PhysicianId = model.SelectedPhysician,
                    });
            }
            return false;
        }

        public async Task<bool> blockRequest(BlockPopUp model)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(model.RequestId);
            requestClient.Status = 0;
            if(await _requestClientRepository.updateRequestClient(requestClient))
            {
                BlockRequest blockRequest = new()
                {
                    PhoneNumber = requestClient.PhoneNumber,
                    Email = requestClient.Email,
                    Reason = model.AdminTransferNotes,
                    RequestId = model.RequestId,
                    CreatedDate = DateTime.Now,
                    IsActive = new BitArray(1, false),
                };
                if (await _requestSatatusLogRepository.addBlockRequest(blockRequest))
                {
                    return await _requestSatatusLogRepository.addRequestSatatusLog(
                        new RequestStatusLog()
                        {
                            RequestId = model.RequestId,
                            Status = 0,
                            CreatedDate = DateTime.Now,
                            Notes = model.AdminTransferNotes,
                        });
                }
            }
            return false;
        }

        public async Task<bool> clearRequest(int requestId)
        {
            RequestClient requestClient = _requestClientRepository.getRequestClientByRequestId(requestId);
            requestClient.Status = 12;
            return await _requestClientRepository.updateRequestClient(requestClient);
        }

        public bool sendAgreement(Agreement model,HttpContext httpContext)
        {
            var request = httpContext.Request;
            List<Claim> claims = new List<Claim>()
            {
                new Claim("requestId", model.RequestId.ToString()),
            };
            String token = _jwtService.genrateJwtTokenForSendMail(claims, DateTime.Now.AddDays(2));
            String link = request.Scheme+"://"+request.Host+"/Admin/Agreement?token=" + token;
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("tatva.dotnet.avinashpatel@outlook.com"),
                Subject = "Agreement",
                IsBodyHtml = true,
                Body = "To Further Proceed to your Request : " + link,
            };
            //mailMessage.To.Add(model.Email);
            mailMessage.To.Add("avinashspatel70@gmail.com");
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Port = 587,
                Credentials = new NetworkCredential(userName: "tatva.dotnet.avinashpatel@outlook.com", password: "Avinash@6351"),
            };
            try
            {
                smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
