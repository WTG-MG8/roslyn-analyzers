﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Analyzer.Utilities;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;

namespace Microsoft.CodeAnalysis.FlowAnalysis.DataFlow
{
    /// <summary>
    /// Contains information about an argument passed to interprocedural analysis.
    /// </summary>
    public sealed class ArgumentInfo<TAbstractAnalysisValue> : CacheBasedEquatable<ArgumentInfo<TAbstractAnalysisValue>>
    {
        public ArgumentInfo(
            IOperation operation,
            AnalysisEntity? analysisEntityOpt,
            PointsToAbstractValue instanceLocation,
            TAbstractAnalysisValue value)
        {
            Operation = operation;
            AnalysisEntityOpt = analysisEntityOpt;
            InstanceLocation = instanceLocation;
            Value = value;
        }

        public IOperation Operation { get; }
        // Can be null for allocations.
        public AnalysisEntity? AnalysisEntityOpt { get; }
        public PointsToAbstractValue InstanceLocation { get; }
        public TAbstractAnalysisValue Value { get; }

        protected override void ComputeHashCodeParts(ref RoslynHashCode hashCode)
        {
            hashCode.Add(Operation.GetHashCode());
            hashCode.Add(AnalysisEntityOpt.GetHashCodeOrDefault());
            hashCode.Add(InstanceLocation.GetHashCode());
            hashCode.Add(Value.GetHashCodeOrDefault());
        }

        protected override bool ComputeEqualsByHashCodeParts(CacheBasedEquatable<ArgumentInfo<TAbstractAnalysisValue>> obj)
        {
            var other = (ArgumentInfo<TAbstractAnalysisValue>)obj;
            return Operation.GetHashCode() == other.Operation.GetHashCode()
                && AnalysisEntityOpt.GetHashCodeOrDefault() == other.AnalysisEntityOpt.GetHashCodeOrDefault()
                && InstanceLocation.GetHashCode() == other.InstanceLocation.GetHashCode()
                && Value.GetHashCodeOrDefault() == other.Value.GetHashCodeOrDefault();
        }
    }
}
