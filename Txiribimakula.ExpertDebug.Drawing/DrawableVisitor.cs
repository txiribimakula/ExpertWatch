﻿using Txiribimakula.ExpertWatch.Geometries;
using Txiribimakula.ExpertWatch.Geometries.Contracts;
using Txiribimakula.ExpertWatch.Drawing.Contracts;

namespace Txiribimakula.ExpertWatch.Drawing
{
    public class DrawableVisitor : IDrawableVisitor
    {
        public ICoordinateSystem CoordinateSystem { get; set; }

        public DrawableVisitor(ICoordinateSystem coordinateSystem) {
            CoordinateSystem = coordinateSystem;
        }

        public ISegment GetTransformedSegment(ISegment segment) {
            IPoint initialPoint = CoordinateSystem.ConvertPointToWorld(segment.InitialPoint);
            IPoint finalPoint = CoordinateSystem.ConvertPointToWorld(segment.FinalPoint);
            return new Segment(initialPoint, finalPoint);
        }

        public IArc GetTransformedArc(IArc arc) {
            IPoint initialPoint = CoordinateSystem.ConvertPointToWorld(arc.InitialPoint);
            IPoint finalPoint = CoordinateSystem.ConvertPointToWorld(arc.FinalPoint);
            IPoint centerPoint = CoordinateSystem.ConvertPointToWorld(arc.CenterPoint);
            float radius = CoordinateSystem.ConvertLengthToWorld(arc.Radius);
            return new Arc(centerPoint, arc.InitialAngle, arc.SweepAngle, radius) {
                InitialPoint = initialPoint,
                FinalPoint = finalPoint
            };
        }

        public IPoint GetTransformedPoint(IPoint point) {
            return CoordinateSystem.ConvertPointToWorld(point); ;
        }
    }
}