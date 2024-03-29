﻿// This file was auto-generated by ML.NET Model Builder.
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace FootballResultModel_PL_EN
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
            [ColumnName(@"Match Number")]
            public float Match_Number { get; set; }

            [LoadColumn(1)]
            [ColumnName(@"Round Number")]
            public float Round_Number { get; set; }

            [LoadColumn(2)]
            [ColumnName(@"Date")]
            public string Date { get; set; }

            [LoadColumn(3)]
            [ColumnName(@"Location")]
            public string Location { get; set; }

            [LoadColumn(4)]
            [ColumnName(@"Home Team")]
            public string Home_Team { get; set; }

            [LoadColumn(5)]
            [ColumnName(@"Away Team")]
            public string Away_Team { get; set; }

            [LoadColumn(6)]
            [ColumnName(@"Result")]
            public string Result { get; set; }

            [LoadColumn(7)]
            [ColumnName(@"HG")]
            public float HG { get; set; }

            [LoadColumn(8)]
            [ColumnName(@"AG")]
            public float AG { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for FootballResultModel.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"Match Number")]
            public float Match_Number { get; set; }

            [ColumnName(@"Round Number")]
            public float Round_Number { get; set; }

            [ColumnName(@"Date")]
            public float[] Date { get; set; }

            [ColumnName(@"Location")]
            public float[] Location { get; set; }

            [ColumnName(@"Home Team")]
            public float[] Home_Team { get; set; }

            [ColumnName(@"Away Team")]
            public float[] Away_Team { get; set; }

            [ColumnName(@"Result")]
            public float[] Result { get; set; }

            [ColumnName(@"HG")]
            public float HG { get; set; }

            [ColumnName(@"AG")]
            public uint AG { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public float PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("FootballResultModel.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

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

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
