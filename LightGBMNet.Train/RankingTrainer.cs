﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using LightGBMNet.Train;
using LightGBMNet.Tree;

namespace LightGBMNet.Train
{

    public sealed class RankingTrainer : TrainerBase<double>
    {
        public override PredictionKind PredictionKind => PredictionKind.Ranking;

        public RankingTrainer(LearningParameters lp, ObjectiveParameters op) : base(lp, op)
        {
            if (op.Objective != ObjectiveType.LambdaRank)
                throw new Exception("Require Objective == ObjectiveType.LambdaRank");
            if (op.Metric == MetricType.DefaultMetric)
                op.Metric = MetricType.Ndcg;
        }

        private protected override IPredictorWithFeatureWeights<double> CreatePredictor()
        {
            return new RankingPredictor(TrainedEnsemble, FeatureCount, AverageOutput);
        }

    }

}
