using Base.Utilities.Business;
using Base.Utilities.Results;
using BusinessLayer.Abstract;
using BusinessLayer.BusinessHelper;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PaySystemManager : PaySystemService
    {
        IVerifyHelper _verifyHelper;
        ICardDal _cardDal;
         ICodeHelper _codeHelper;
        public PaySystemManager(ICardDal cardDal, IVerifyHelper helper,ICodeHelper codeHelper)
        {
            _cardDal = cardDal;
            _verifyHelper = helper;
            _codeHelper = codeHelper;
        }
        public IResult CheckAndPay(Card card)
        {
            var result = BusinessRule.Run(CheckCard(card));
            if (result.IsSuccess != false)
            {
                var code = _codeHelper.GenerateVerificationCode();
                return new SuccessResult(code);

            }
            else
            {
                return new ErrorResult("Kart bilgileri yanlış");
            }


        }

        public IResult VerifyCode( string enteredCode)
        {
            bool isCodeValid = _verifyHelper.VerifyCode(enteredCode);
            if (!isCodeValid)
            {
                return new ErrorResult("Doğrulama kodu yanlış.");
            }
            string token = Guid.NewGuid().ToString();
            return new SuccessResult(token);
        }
        
        private IResult CheckCard(Card card)
        {
            var customerCard = _cardDal.GetCustomerInfoCard(c => c.CardNumber == card.CardNumber);
            if (customerCard == null || card.CardNumber != customerCard.CardNumber || card.CardholderName.ToLower() != customerCard.CardholderName.ToLower() || card.CVVCVC != customerCard.CVVCVC)
            {
                return new ErrorResult("Bilgiler Yanlış");
            }
            else
            {
                
                return new SuccessResult("Bilgiler Doğru ");
            }
        }


    }
}
