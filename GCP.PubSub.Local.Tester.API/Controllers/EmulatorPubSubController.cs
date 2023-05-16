using Microsoft.AspNetCore.Mvc;

namespace GCP.Local.Tester.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmulatorPubSubController : ControllerBase
    {
        private EmuladorPubSub _emuladorPubSub;

        public EmulatorPubSubController()
        {
            _emuladorPubSub = new EmuladorPubSub();
        }
        /// <summary>
        /// M�todo respons�vel por criar um t�pico no emulador do Pub/Sub GCP    
        /// </summary>
        /// <param name="topicId">Id do t�pico a ser criado.</param>
        [HttpPost("/createTopic")]
        public void CreateTopic(string topicId)
        {
            _emuladorPubSub.CreateTopic(topicId);
        }

        /// <summary>
        /// M�todo respons�vel pela cria��o de uma subscriber, para leitura de mensagens enviadas a um t�pico.
        /// </summary>
        /// <param name="subscriptionId">Id do subscriber a ser criado.</param>
        /// <param name="topicId">Id do t�pico a ser consumido pela subscriber</param>
        /// <param name="pushEndpoint">Voc� pode informar um endpoint (webhook) para receber qualquer mensagem que for postada no t�pico. <br></br>
        /// O endpoint informado deve esperar um m�todo POST. Segue abaixo um exemplo de mensagem enviada para o endpoint push: <br></br>
        /// {<br></br>   
        ///     \"subscription\":\"id subscription",<br></br>   
        ///     \"message\":{<br></br>      
        ///         \"data\":\"texto encriptografado base64\",<br></br>      
        ///         \"messageId\":\"1\",<br></br>      
        ///         \"attributes\":{<br></br>         
        ///          }<br></br>   
        ///     }<br></br>
        /// }
        /// </param>
        /// <returns></returns>
        [HttpPost("/createSubscriber")]
        public async Task CreateSubscriber(string subscriptionId,string topicId, string pushEndpoint)
        {
            await _emuladorPubSub.CreateSubscriber(subscriptionId, topicId, pushEndpoint);
        }

        /// <summary>
        /// M�todo respons�vel por fazer a publica��o da mensagem
        /// </summary>
        /// <param name="topicId">Id do t�pico a ser enviada a mensagem</param>
        /// <param name="message">Mensagem</param>
        /// <returns></returns>
        [HttpPost("/publishMessage")]
        public async Task PublishMessage(string topicId, string message)
        {
            await _emuladorPubSub.PublishMessage(topicId,message);
        }
    }
}