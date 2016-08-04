using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CondoSimples.SMS
{
    public class SMSHandler
    {
        public static bool SendSMS(string number, ref string msgErro)
        {
            bool functionReturnValue = false;

            string Retorno = null;
            string URL = "";
            string UserID = "beb6eba3-23cd-47fe-bfbd-270380d16634";
            string Token = "57879074";
            string message = "Você recebeu uma encomenda";

            //Codifica a mensagem
            message = System.Web.HttpUtility.UrlEncode(message);

            //Monta a URL para dar o GET 
            URL = "http://www.misterpostman.com.br/gateway.aspx?UserID=" + UserID + "&Token=" + Token + "&NroDestino=" + number + "&Mensagem=" + message;


            try
            {
                // Dá um GET na URL usando objeto WebClient
                WebClient webClient = new WebClient();
                Stream mystream = webClient.OpenRead(URL);
                StreamReader sr = new StreamReader(mystream);
                // Lê a resposta enviada pelo WebService
                Retorno = sr.ReadToEnd();

                // Verifica se executou com sucesso - procura padrão "OK"
                if (((Retorno.IndexOf("OK") + 1) > 0))
                {
                    functionReturnValue = true;
                }
                else
                {
                    msgErro = Retorno;
                    functionReturnValue = false;
                }

            }
            catch (Exception ex)
            {
                functionReturnValue = false;
                msgErro = ex.Message;

            }
            return functionReturnValue;

        }
    }
}