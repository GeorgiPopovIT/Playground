﻿// This file was auto-generated by ML.NET Model Builder.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using Microsoft.ML;

namespace FootballResultModel_Cдадс
{
    public partial class FootballResultModel
    {
        public const string RetrainFilePath =  @"C:\Users\Georges\Desktop\Tests.Net\Playground\ML.NET-Test\FootballMatches_Predictor\data\Germany-Bundesliga-1-Season-2022-2023-Fixture.csv";
        public const char RetrainSeparatorChar = ',';
        public const bool RetrainHasHeader =  true;

         /// <summary>
        /// Train a new model with the provided dataset.
        /// </summary>
        /// <param name="outputModelPath">File path for saving the model. Should be similar to "C:\YourPath\ModelName.mlnet"</param>
        /// <param name="inputDataFilePath">Path to the data file for training.</param>
        /// <param name="separatorChar">Separator character for delimited training file.</param>
        /// <param name="hasHeader">Boolean if training file has a header.</param>
        public static void Train(string outputModelPath, string inputDataFilePath = RetrainFilePath, char separatorChar = RetrainSeparatorChar, bool hasHeader = RetrainHasHeader)
        {
            var mlContext = new MLContext();

            var data = LoadIDataViewFromFile(mlContext, inputDataFilePath, separatorChar, hasHeader);
            var model = RetrainModel(mlContext, data);
            SaveModel(mlContext, model, data, outputModelPath);
        }

        /// <summary>
        /// Load an IDataView from a file path.
        /// </summary>
        /// <param name="mlContext">The common context for all ML.NET operations.</param>
        /// <param name="inputDataFilePath">Path to the data file for training.</param>
        /// <param name="separatorChar">Separator character for delimited training file.</param>
        /// <param name="hasHeader">Boolean if training file has a header.</param>
        /// <returns>IDataView with loaded training data.</returns>
        public static IDataView LoadIDataViewFromFile(MLContext mlContext, string inputDataFilePath, char separatorChar, bool hasHeader)
        {
            return mlContext.Data.LoadFromTextFile<ModelInput>(inputDataFilePath, separatorChar, hasHeader);
        }



        /// <summary>
        /// Save a model at the specified path.
        /// </summary>
        /// <param name="mlContext">The common context for all ML.NET operations.</param>
        /// <param name="model">Model to save.</param>
        /// <param name="data">IDataView used to train the model.</param>
        /// <param name="modelSavePath">File path for saving the model. Should be similar to "C:\YourPath\ModelName.mlnet.</param>
        public static void SaveModel(MLContext mlContext, ITransformer model, IDataView data, string modelSavePath)
        {
            // Pull the data schema from the IDataView used for training the model
            DataViewSchema dataViewSchema = data.Schema;

            using (var fs = File.Create(modelSavePath))
            {
                mlContext.Model.Save(model, dataViewSchema, fs);
            }
        }


        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainModel(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }


        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(new []{new InputOutputColumnPair(@"Nation", @"Nation"),new InputOutputColumnPair(@"League", @"League"),new InputOutputColumnPair(@"FTR", @"FTR")}, outputKind: OneHotEncodingEstimator.OutputKind.Indicator)      
                                    .Append(mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"Date", @"Date"),new InputOutputColumnPair(@"Month", @"Month"),new InputOutputColumnPair(@"Year", @"Year"),new InputOutputColumnPair(@"AG", @"AG"),new InputOutputColumnPair(@"MissingHomeCount", @"MissingHomeCount"),new InputOutputColumnPair(@"MissingAwayCount", @"MissingAwayCount"),new InputOutputColumnPair(@"Bet365HC", @"Bet365HC"),new InputOutputColumnPair(@"Bet365DC", @"Bet365DC"),new InputOutputColumnPair(@"Bet365AC", @"Bet365AC"),new InputOutputColumnPair(@"YCH", @"YCH"),new InputOutputColumnPair(@"YCA", @"YCA"),new InputOutputColumnPair(@"RCH", @"RCH"),new InputOutputColumnPair(@"RCA", @"RCA"),new InputOutputColumnPair(@"FirstHalfHome", @"FirstHalfHome"),new InputOutputColumnPair(@"FirstHalfAway", @"FirstHalfAway")}))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Home",outputColumnName:@"Home"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Away",outputColumnName:@"Away"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Nation",@"League",@"FTR",@"Date",@"Month",@"Year",@"AG",@"MissingHomeCount",@"MissingAwayCount",@"Bet365HC",@"Bet365DC",@"Bet365AC",@"YCH",@"YCA",@"RCH",@"RCA",@"FirstHalfHome",@"FirstHalfAway",@"Home",@"Away"}))      
                                    .Append(mlContext.Regression.Trainers.FastForest(new FastForestRegressionTrainer.Options(){NumberOfTrees=4,NumberOfLeaves=4,FeatureFraction=1F,LabelColumnName=@"HG",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
 }
