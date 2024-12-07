﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalamaTravelDTA;

namespace SalamaTravelBL
{
   public class InternalBalanceBL
    {
        public static async Task<bool>PayWithInternalBalanceAsync(SalamaTravelDTA.InternalBalanceDAL.PaymentService.PayInternalBalanceRequest Req)
        {
            return await SalamaTravelDTA.InternalBalanceDAL.PaymentService.PayWithInternalBalanceAsync(Req.Token,Req.ClientId,Req.Amount);
        }
        public static async Task<bool> PayDues(SalamaTravelDTA.InternalBalanceDAL.PaymentService.PayDuesRequest Req)
        {
            return await SalamaTravelDTA.InternalBalanceDAL.PaymentService.PayDuesAsync(Req.Token, Req.ClientId);
        }
        public static async Task<bool>UpdateInternalBalanceAsync(InternalBalanceDAL.PaymentService.PayInternalBalanceRequest request)
        {
            return await InternalBalanceDAL.PaymentService.UpdateInternalBalanceAsync(request.Token, request.ClientId, request.Amount);
        }
    }
}
