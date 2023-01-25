﻿// This file was auto-generated by ML.NET Model Builder.
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace Bundesliga_AG
{
    public partial class FootballResultModel
    {
        /// <summary>
        /// model input class for FootballResultModel.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [LoadColumn(0)]
            [ColumnName(@"Nation")]
            public string Nation { get; set; }

            [LoadColumn(1)]
            [ColumnName(@"League")]
            public string League { get; set; }

            [LoadColumn(2)]
            [ColumnName(@"Date")]
            public float Date { get; set; }

            [LoadColumn(3)]
            [ColumnName(@"Month")]
            public float Month { get; set; }

            [LoadColumn(4)]
            [ColumnName(@"Year")]
            public float Year { get; set; }

            [LoadColumn(5)]
            [ColumnName(@"Home")]
            public string Home { get; set; }

            [LoadColumn(6)]
            [ColumnName(@"Away")]
            public string Away { get; set; }

            [LoadColumn(7)]
            [ColumnName(@"HG")]
            public float HG { get; set; }

            [LoadColumn(8)]
            [ColumnName(@"AG")]
            public float AG { get; set; }

            [LoadColumn(9)]
            [ColumnName(@"FTR")]
            public string FTR { get; set; }

            [LoadColumn(10)]
            [ColumnName(@"MissingHomeCount")]
            public float MissingHomeCount { get; set; }

            [LoadColumn(11)]
            [ColumnName(@"MissingAwayCount")]
            public float MissingAwayCount { get; set; }

            [LoadColumn(12)]
            [ColumnName(@"Bet365HC")]
            public float Bet365HC { get; set; }

            [LoadColumn(13)]
            [ColumnName(@"Bet365DC")]
            public float Bet365DC { get; set; }

            [LoadColumn(14)]
            [ColumnName(@"Bet365AC")]
            public float Bet365AC { get; set; }

            [LoadColumn(15)]
            [ColumnName(@"YCH")]
            public float YCH { get; set; }

            [LoadColumn(16)]
            [ColumnName(@"YCA")]
            public float YCA { get; set; }

            [LoadColumn(17)]
            [ColumnName(@"RCH")]
            public float RCH { get; set; }

            [LoadColumn(18)]
            [ColumnName(@"RCA")]
            public float RCA { get; set; }

            [LoadColumn(19)]
            [ColumnName(@"FirstHalfHome")]
            public float FirstHalfHome { get; set; }

            [LoadColumn(20)]
            [ColumnName(@"FirstHalfAway")]
            public float FirstHalfAway { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for FootballResultModel.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"Nation")]
            public float[] Nation { get; set; }

            [ColumnName(@"League")]
            public float[] League { get; set; }

            [ColumnName(@"Date")]
            public float Date { get; set; }

            [ColumnName(@"Month")]
            public float Month { get; set; }

            [ColumnName(@"Year")]
            public float Year { get; set; }

            [ColumnName(@"Home")]
            public float[] Home { get; set; }

            [ColumnName(@"Away")]
            public float[] Away { get; set; }

            [ColumnName(@"HG")]
            public float HG { get; set; }

            [ColumnName(@"AG")]
            public float AG { get; set; }

            [ColumnName(@"FTR")]
            public float[] FTR { get; set; }

            [ColumnName(@"MissingHomeCount")]
            public float MissingHomeCount { get; set; }

            [ColumnName(@"MissingAwayCount")]
            public float MissingAwayCount { get; set; }

            [ColumnName(@"Bet365HC")]
            public float Bet365HC { get; set; }

            [ColumnName(@"Bet365DC")]
            public float Bet365DC { get; set; }

            [ColumnName(@"Bet365AC")]
            public float Bet365AC { get; set; }

            [ColumnName(@"YCH")]
            public float YCH { get; set; }

            [ColumnName(@"YCA")]
            public float YCA { get; set; }

            [ColumnName(@"RCH")]
            public float RCH { get; set; }

            [ColumnName(@"RCA")]
            public float RCA { get; set; }

            [ColumnName(@"FirstHalfHome")]
            public float FirstHalfHome { get; set; }

            [ColumnName(@"FirstHalfAway")]
            public float FirstHalfAway { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"Score")]
            public float Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("FootballResultModel.mlnet");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);


        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

    }
}
