﻿// Copyright 2015, 2014 Matthias Koch
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace Example._104_PaymentController
{
  public class PaymentController
  {
    readonly IPaymentService _paymentService;

    public PaymentController (IPaymentService paymentService)
    {
      _paymentService = paymentService;
    }

    public string Pay (PaymentModel model)
    {
      return _paymentService.PayWithCreditCard (model.Owner, model.Number, model.Validity, model.Cvc)
          ? "Success"
          : "Error";
    }
  }
}