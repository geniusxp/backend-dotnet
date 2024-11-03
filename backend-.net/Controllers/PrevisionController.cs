using Microsoft.AspNetCore.Mvc;
using geniusxp_backend_dotnet.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace geniusxp_backend_dotnet.Controllers
{
    public class EventDataInput
    {
        [LoadColumn(1)]
        public float NumberOfParticipants { get; set; }

        [LoadColumn(2)]
        public string EventType { get; set; }

        [LoadColumn(3)]
        public float NumberOfActivities { get; set; }

        [LoadColumn(4)]
        public DateTime EventDate { get; set; }
    }

    public class EventData
    {
        [LoadColumn(0)]
        public float Duration { get; set; }

        [LoadColumn(1)]
        public float NumberOfParticipants { get; set; }

        [LoadColumn(2)]
        public string EventType { get; set; }

        [LoadColumn(3)]
        public float NumberOfActivities { get; set; }

        [LoadColumn(4)]
        public DateTime EventDate { get; set; }
    }

    public class DurationPrediction
    {
        [ColumnName("Score")]
        public float PredictedDuration { get; set; }
    }

    [Route("api/[controller]")]
    [SwaggerTag("Controller de Previsão")]
    [Authorize]
    [ApiController]
    public class EventPredictionController : ControllerBase
    {
        private readonly string _modelPath;
        private readonly string _trainingDataPath;
        private readonly MLContext _mlContext;

        public EventPredictionController()
        {
            _mlContext = new MLContext();
            _modelPath = Path.Combine(Environment.CurrentDirectory, "MLModels", "EventDurationModel.zip");
            _trainingDataPath = Path.Combine(Environment.CurrentDirectory, "Datasets", "event_train_data.csv");

            if (!System.IO.File.Exists(_modelPath))
            {
                TrainModel();
            }
        }

        private void TrainModel()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_modelPath) ?? string.Empty);
            var trainingData = _mlContext.Data.LoadFromTextFile<EventData>(_trainingDataPath, hasHeader: true, separatorChar: ',');

            var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding("EventTypeEncoded", nameof(EventData.EventType))
                .Append(_mlContext.Transforms.Concatenate("Features",
                    nameof(EventData.NumberOfParticipants),
                    "EventTypeEncoded",
                    nameof(EventData.NumberOfActivities)))
                .Append(_mlContext.Regression.Trainers.FastTree(labelColumnName: nameof(EventData.Duration)));

            var model = pipeline.Fit(trainingData);
            _mlContext.Model.Save(model, trainingData.Schema, _modelPath);
        }

        [HttpPost("predict")]
        [SwaggerOperation(
            Summary = "Prediz a duração de seu evento",
            Description = "Prevemos a duração ideal de seu evento com base no número de participantes, tipo de evento e número de atividades. Os tipos de eventos aceitos são 'Seminário', 'Webinar', 'Conferência' e 'Workshop'."
        )]
        public ActionResult<string> PredictEventDuration([FromBody] EventDataInput eventData)
        {
            if (!System.IO.File.Exists(_modelPath))
                return BadRequest("Modelo não treinado.");

            if (eventData.NumberOfParticipants <= 0)
                return BadRequest("O número de participantes deve ser maior que zero.");

            if (string.IsNullOrWhiteSpace(eventData.EventType) ||
                !(eventData.EventType.Equals("Seminário", StringComparison.OrdinalIgnoreCase) ||
                  eventData.EventType.Equals("Webinar", StringComparison.OrdinalIgnoreCase) ||
                  eventData.EventType.Equals("Conferência", StringComparison.OrdinalIgnoreCase) ||
                  eventData.EventType.Equals("Workshop", StringComparison.OrdinalIgnoreCase)))
                return BadRequest("O tipo de evento deve ser Seminário, Webinar, Conferência ou Workshop.");

            if (eventData.NumberOfActivities <= 0)
                return BadRequest("O número de atividades deve ser maior que zero.");

            var prediction = PredictDuration(eventData);
            string responseMessage = FormatDurationPredictionResponse(prediction);

            return Ok($"A previsão ideal para seu evento é de {responseMessage}.");
        }

        private DurationPrediction PredictDuration(EventDataInput eventData)
        {
            using var stream = new FileStream(_modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var model = _mlContext.Model.Load(stream, out _);
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<EventData, DurationPrediction>(model);
            return predictionEngine.Predict(new EventData
            {
                NumberOfParticipants = eventData.NumberOfParticipants,
                EventType = eventData.EventType,
                NumberOfActivities = eventData.NumberOfActivities,
                EventDate = eventData.EventDate
            });
        }

        private string FormatDurationPredictionResponse(DurationPrediction prediction)
        {
            float predictedDuration = (float)Math.Round(prediction.PredictedDuration);

            if (predictedDuration >= 24)
            {
                int days = (int)(predictedDuration / 24);
                int hours = (int)(predictedDuration % 24);
                return $"{days} dia(s) e {hours} hora(s)";
            }
            else
            {
                return $"{(int)predictedDuration} hora(s)";
            }
        }
    }
}
