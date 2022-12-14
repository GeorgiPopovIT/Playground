﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using Microsoft.ML;

namespace Football_Matches_Predictor
{
    public partial class MatchModelmbconfig
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
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
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(new []{new InputOutputColumnPair(@"Div", @"Div"),new InputOutputColumnPair(@"Time", @"Time"),new InputOutputColumnPair(@"FTR", @"FTR"),new InputOutputColumnPair(@"HTR", @"HTR")}, outputKind: OneHotEncodingEstimator.OutputKind.Indicator)      
                                    .Append(mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"FTAG", @"FTAG"),new InputOutputColumnPair(@"HTHG", @"HTHG"),new InputOutputColumnPair(@"HTAG", @"HTAG"),new InputOutputColumnPair(@"HS", @"HS"),new InputOutputColumnPair(@"AS", @"AS"),new InputOutputColumnPair(@"HST", @"HST"),new InputOutputColumnPair(@"AST", @"AST"),new InputOutputColumnPair(@"HF", @"HF"),new InputOutputColumnPair(@"AF", @"AF"),new InputOutputColumnPair(@"HC", @"HC"),new InputOutputColumnPair(@"AC", @"AC"),new InputOutputColumnPair(@"HY", @"HY"),new InputOutputColumnPair(@"AY", @"AY"),new InputOutputColumnPair(@"HR", @"HR"),new InputOutputColumnPair(@"AR", @"AR"),new InputOutputColumnPair(@"B365H", @"B365H"),new InputOutputColumnPair(@"B365D", @"B365D"),new InputOutputColumnPair(@"B365A", @"B365A"),new InputOutputColumnPair(@"BWH", @"BWH"),new InputOutputColumnPair(@"BWD", @"BWD"),new InputOutputColumnPair(@"BWA", @"BWA"),new InputOutputColumnPair(@"IWH", @"IWH"),new InputOutputColumnPair(@"IWD", @"IWD"),new InputOutputColumnPair(@"IWA", @"IWA"),new InputOutputColumnPair(@"PSH", @"PSH"),new InputOutputColumnPair(@"PSD", @"PSD"),new InputOutputColumnPair(@"PSA", @"PSA"),new InputOutputColumnPair(@"WHH", @"WHH"),new InputOutputColumnPair(@"WHD", @"WHD"),new InputOutputColumnPair(@"WHA", @"WHA"),new InputOutputColumnPair(@"VCH", @"VCH"),new InputOutputColumnPair(@"VCD", @"VCD"),new InputOutputColumnPair(@"VCA", @"VCA"),new InputOutputColumnPair(@"MaxH", @"MaxH"),new InputOutputColumnPair(@"MaxD", @"MaxD"),new InputOutputColumnPair(@"MaxA", @"MaxA"),new InputOutputColumnPair(@"AvgH", @"AvgH"),new InputOutputColumnPair(@"AvgD", @"AvgD"),new InputOutputColumnPair(@"AvgA", @"AvgA"),new InputOutputColumnPair(@"B365>2.5", @"B365>2.5"),new InputOutputColumnPair(@"B365<2.5", @"B365<2.5"),new InputOutputColumnPair(@"P>2.5", @"P>2.5"),new InputOutputColumnPair(@"P<2.5", @"P<2.5"),new InputOutputColumnPair(@"Max>2.5", @"Max>2.5"),new InputOutputColumnPair(@"Max<2.5", @"Max<2.5"),new InputOutputColumnPair(@"Avg>2.5", @"Avg>2.5"),new InputOutputColumnPair(@"Avg<2.5", @"Avg<2.5"),new InputOutputColumnPair(@"AHh", @"AHh"),new InputOutputColumnPair(@"B365AHH", @"B365AHH"),new InputOutputColumnPair(@"B365AHA", @"B365AHA"),new InputOutputColumnPair(@"PAHH", @"PAHH"),new InputOutputColumnPair(@"PAHA", @"PAHA"),new InputOutputColumnPair(@"MaxAHH", @"MaxAHH"),new InputOutputColumnPair(@"MaxAHA", @"MaxAHA"),new InputOutputColumnPair(@"AvgAHH", @"AvgAHH"),new InputOutputColumnPair(@"AvgAHA", @"AvgAHA"),new InputOutputColumnPair(@"B365CH", @"B365CH"),new InputOutputColumnPair(@"B365CD", @"B365CD"),new InputOutputColumnPair(@"B365CA", @"B365CA"),new InputOutputColumnPair(@"BWCH", @"BWCH"),new InputOutputColumnPair(@"BWCD", @"BWCD"),new InputOutputColumnPair(@"BWCA", @"BWCA"),new InputOutputColumnPair(@"IWCH", @"IWCH"),new InputOutputColumnPair(@"IWCD", @"IWCD"),new InputOutputColumnPair(@"IWCA", @"IWCA"),new InputOutputColumnPair(@"PSCH", @"PSCH"),new InputOutputColumnPair(@"PSCD", @"PSCD"),new InputOutputColumnPair(@"PSCA", @"PSCA"),new InputOutputColumnPair(@"WHCH", @"WHCH"),new InputOutputColumnPair(@"WHCD", @"WHCD"),new InputOutputColumnPair(@"WHCA", @"WHCA"),new InputOutputColumnPair(@"VCCH", @"VCCH"),new InputOutputColumnPair(@"VCCD", @"VCCD"),new InputOutputColumnPair(@"VCCA", @"VCCA"),new InputOutputColumnPair(@"MaxCH", @"MaxCH"),new InputOutputColumnPair(@"MaxCD", @"MaxCD"),new InputOutputColumnPair(@"MaxCA", @"MaxCA"),new InputOutputColumnPair(@"AvgCH", @"AvgCH"),new InputOutputColumnPair(@"AvgCD", @"AvgCD"),new InputOutputColumnPair(@"AvgCA", @"AvgCA"),new InputOutputColumnPair(@"B365C>2.5", @"B365C>2.5"),new InputOutputColumnPair(@"B365C<2.5", @"B365C<2.5"),new InputOutputColumnPair(@"PC>2.5", @"PC>2.5"),new InputOutputColumnPair(@"PC<2.5", @"PC<2.5"),new InputOutputColumnPair(@"MaxC>2.5", @"MaxC>2.5"),new InputOutputColumnPair(@"MaxC<2.5", @"MaxC<2.5"),new InputOutputColumnPair(@"AvgC>2.5", @"AvgC>2.5"),new InputOutputColumnPair(@"AvgC<2.5", @"AvgC<2.5"),new InputOutputColumnPair(@"AHCh", @"AHCh"),new InputOutputColumnPair(@"B365CAHH", @"B365CAHH"),new InputOutputColumnPair(@"B365CAHA", @"B365CAHA"),new InputOutputColumnPair(@"PCAHH", @"PCAHH"),new InputOutputColumnPair(@"PCAHA", @"PCAHA"),new InputOutputColumnPair(@"MaxCAHH", @"MaxCAHH"),new InputOutputColumnPair(@"MaxCAHA", @"MaxCAHA"),new InputOutputColumnPair(@"AvgCAHH", @"AvgCAHH"),new InputOutputColumnPair(@"AvgCAHA", @"AvgCAHA")}))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Date",outputColumnName:@"Date"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"HomeTeam",outputColumnName:@"HomeTeam"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"AwayTeam",outputColumnName:@"AwayTeam"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Div",@"Time",@"FTR",@"HTR",@"FTAG",@"HTHG",@"HTAG",@"HS",@"AS",@"HST",@"AST",@"HF",@"AF",@"HC",@"AC",@"HY",@"AY",@"HR",@"AR",@"B365H",@"B365D",@"B365A",@"BWH",@"BWD",@"BWA",@"IWH",@"IWD",@"IWA",@"PSH",@"PSD",@"PSA",@"WHH",@"WHD",@"WHA",@"VCH",@"VCD",@"VCA",@"MaxH",@"MaxD",@"MaxA",@"AvgH",@"AvgD",@"AvgA",@"B365>2.5",@"B365<2.5",@"P>2.5",@"P<2.5",@"Max>2.5",@"Max<2.5",@"Avg>2.5",@"Avg<2.5",@"AHh",@"B365AHH",@"B365AHA",@"PAHH",@"PAHA",@"MaxAHH",@"MaxAHA",@"AvgAHH",@"AvgAHA",@"B365CH",@"B365CD",@"B365CA",@"BWCH",@"BWCD",@"BWCA",@"IWCH",@"IWCD",@"IWCA",@"PSCH",@"PSCD",@"PSCA",@"WHCH",@"WHCD",@"WHCA",@"VCCH",@"VCCD",@"VCCA",@"MaxCH",@"MaxCD",@"MaxCA",@"AvgCH",@"AvgCD",@"AvgCA",@"B365C>2.5",@"B365C<2.5",@"PC>2.5",@"PC<2.5",@"MaxC>2.5",@"MaxC<2.5",@"AvgC>2.5",@"AvgC<2.5",@"AHCh",@"B365CAHH",@"B365CAHA",@"PCAHH",@"PCAHA",@"MaxCAHH",@"MaxCAHA",@"AvgCAHH",@"AvgCAHA",@"Date",@"HomeTeam",@"AwayTeam"}))      
                                    .Append(mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))      
                                    .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression(new LbfgsPoissonRegressionTrainer.Options(){L1Regularization=1.598967F,L2Regularization=0.03125F,LabelColumnName=@"FTHG",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
}
