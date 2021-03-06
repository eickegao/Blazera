﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace BlazeraLib
{
    public class SpeechBubbleShape
    {
        #region Constants

        const UInt32 BASE_POINT_COUNT = 5;
        const Byte BACKGROUND_COLOR_ALPHA = 192;

        const BorderType DEFAULT_TIP_BORDER_TYPE = BorderType.Bottom;
        const float DEFAULT_SHADOW_OFFSET = 4F;

        static readonly Color EFFECT_BEGIN_COLOR = Color.White;
        static readonly Color EFFECT_END_COLOR = Color.Black;
        static readonly Color SHADOW_EFFECT_COLOR = new Color(0, 0, 0, 128);

        const float DIFFERENCE_RADIUS_SIZE_LIMIT = 10F;
        const float MIN_SIZE = 20F;

        const float DEFAULT_TIP_POSITION = 50F;
        const float DEFAULT_TIP_SIZE = 8F;

        public const float TIP_LENGTH_SCALE_FACTOR = 2F;

        #endregion

        #region Members

        Shape Background;
        Shape Tip;
        Shape Effect;
        Shape EffectTip;
        Shape ShadowEffect;
        Shape ShadowEffectTip;

        Boolean TipMode;

        Vector2f Position;
        Vector2f Dimension;

        float Radius;
        float OutlineThickness;

        Color BackgroundColor;
        Color OutlineColor;

        float TipPosition;
        float TipSize;

        public BorderType TipBorderType { get; private set; }

        Boolean ShadowMode;
        Vector2f ShadowOffset;

        #endregion

        public SpeechBubbleShape(
            Vector2f dimension,
            float radius,
            float outlineThickness,
            Color backgroundColor,
            Color outlineColor,
            Boolean tipMode = false,
            BorderType tipBorderType = DEFAULT_TIP_BORDER_TYPE,
            Boolean shadowMode = false,
            float shadowOffset = DEFAULT_SHADOW_OFFSET,
            float tipPosition = DEFAULT_TIP_POSITION,
            float tipSize = DEFAULT_TIP_SIZE)
        {
            Background = new Shape();
            Background.EnableFill(true);
            Background.EnableOutline(true);
            Tip = new Shape();
            Tip.EnableFill(true);

            Effect = new Shape();
            Effect.EnableFill(true);
            Effect.EnableOutline(true);
            EffectTip = new Shape();

            ShadowEffect = new Shape();
            ShadowEffect.EnableFill(true);
            ShadowEffect.EnableOutline(true);
            ShadowEffectTip = new Shape();
            ShadowEffectTip.EnableFill(true);
            ShadowEffectTip.EnableOutline(false);

            Dimension = dimension;

            ShadowMode = shadowMode;
            ShadowOffset = new Vector2f(shadowOffset, shadowOffset);

            Radius = radius;
            OutlineThickness = outlineThickness;

            AdjustSize();

            TipMode = tipMode;
            TipBorderType = tipBorderType;

            BackgroundColor = backgroundColor;
            BackgroundColor.A = BACKGROUND_COLOR_ALPHA;
            OutlineColor = outlineColor;
            OutlineColor.A = BACKGROUND_COLOR_ALPHA;

            Background.OutlineThickness = OutlineThickness;
            Effect.OutlineThickness = OutlineThickness;
            ShadowEffect.OutlineThickness = OutlineThickness;

            TipPosition = tipPosition;
            TipSize = tipSize;

            Build();
        }

        void AdjustSize()
        {
            if (Dimension.X < MIN_SIZE)
                Dimension.X = MIN_SIZE;
            if (Dimension.Y < MIN_SIZE)
                Dimension.Y = MIN_SIZE;

            while (Dimension.X < Radius * 2F + DIFFERENCE_RADIUS_SIZE_LIMIT)
                --Radius;
            while (Dimension.Y < Radius * 2F + DIFFERENCE_RADIUS_SIZE_LIMIT)
                --Radius;
        }

        Vector2f GetCenter(CornerType cornerType)
        {
            Vector2f center = new Vector2f();

            switch (cornerType)
            {
                case CornerType.TopLeft: center = Position + new Vector2f(Radius, Radius); break;
                case CornerType.TopRight: center = new Vector2f(Position.X + Dimension.X - Radius, Position.Y + Radius); break;
                case CornerType.BottomRight: center = Position + Dimension - new Vector2f(Radius, Radius); break;
                case CornerType.BottomLeft: center = new Vector2f(Position.X + Radius, Position.Y + Dimension.Y - Radius); break;
            }

            return center;
        }

        Vector2f GetTipPosition()
        {
            Vector2f tipPosition = new Vector2f();

            switch (TipBorderType)
            {
                case BorderType.Top: tipPosition = new Vector2f(Position.X + TipPosition * Dimension.X / 100F - GetTipDimension().X / 2F, Position.Y - OutlineThickness); break;
                case BorderType.Left:
                case BorderType.Right:
                case BorderType.Bottom: tipPosition = new Vector2f(Position.X + TipPosition * Dimension.X / 100F + GetTipDimension().X / 2F, Position.Y + Dimension.Y + OutlineThickness); break;
            }

            return tipPosition;
        }

        Vector2f GetTipDimension()
        {
            Vector2f tipDimension = new Vector2f();

            switch (TipBorderType)
            {
                case BorderType.Top: tipDimension = new Vector2f(TipSize * Dimension.X / 100F, TipSize * Dimension.X / 100F * TIP_LENGTH_SCALE_FACTOR); break;
                case BorderType.Left:
                case BorderType.Right:
                case BorderType.Bottom: tipDimension = new Vector2f(TipSize * Dimension.X / 100F, TipSize * Dimension.X / 100F * TIP_LENGTH_SCALE_FACTOR); break;
            }

            return tipDimension;
        }

        void Build()
        {
            Vector2f tipPosition = GetTipPosition();
            Vector2f tipDimension = GetTipDimension();

            UInt32 pointCount = (UInt32)(BASE_POINT_COUNT * Radius);
            // top left
            for (UInt32 count = pointCount / 2; count < pointCount * .75; ++count)
            {
                float angle = count * 2 * (float)Math.PI / pointCount;
                Vector2f offset = new Vector2f((float)Math.Cos(angle), (float)Math.Sin(angle));

                Vector2f center = GetCenter(CornerType.TopLeft);

                Background.AddPoint(center + offset * Radius, BackgroundColor, OutlineColor);
                Effect.AddPoint(center + offset * Radius, EFFECT_BEGIN_COLOR, EFFECT_END_COLOR);
                ShadowEffect.AddPoint(center + offset * Radius, SHADOW_EFFECT_COLOR, SHADOW_EFFECT_COLOR);
            }

            if (TipMode && TipBorderType == BorderType.Top)
            {
                Tip.AddPoint(tipPosition, BackgroundColor);
                Tip.AddPoint(tipPosition + new Vector2f(tipDimension.X / 2F, -tipDimension.Y), BackgroundColor);
                Tip.AddPoint(tipPosition + new Vector2f(tipDimension.X, 0F), BackgroundColor);

                EffectTip.AddPoint(tipPosition, EFFECT_BEGIN_COLOR);
                EffectTip.AddPoint(tipPosition + new Vector2f(tipDimension.X / 2F, -tipDimension.Y), EFFECT_BEGIN_COLOR);
                EffectTip.AddPoint(tipPosition + new Vector2f(tipDimension.X, 0F), EFFECT_BEGIN_COLOR);

                ShadowEffectTip.AddPoint(tipPosition, SHADOW_EFFECT_COLOR);
                ShadowEffectTip.AddPoint(tipPosition + new Vector2f(tipDimension.X / 2F, -tipDimension.Y), SHADOW_EFFECT_COLOR);
                ShadowEffectTip.AddPoint(tipPosition + new Vector2f(tipDimension.X, 0F), SHADOW_EFFECT_COLOR);

                if (TipMode)
                {
                    Background.AddPoint(tipPosition + new Vector2f(-1F, OutlineThickness), BackgroundColor, OutlineColor);
                    for (float count = 0; count < tipDimension.X; ++count)
                        Background.AddPoint(tipPosition + new Vector2f(count, OutlineThickness), BackgroundColor, BackgroundColor);
                    Background.AddPoint(tipPosition + new Vector2f(tipDimension.X + 1F, OutlineThickness), BackgroundColor, OutlineColor);

                    Effect.AddPoint(tipPosition + new Vector2f(-1F, OutlineThickness), EFFECT_BEGIN_COLOR, EFFECT_END_COLOR);
                    for (float count = 0; count < tipDimension.X; ++count)
                        Effect.AddPoint(tipPosition + new Vector2f(count, OutlineThickness), EFFECT_BEGIN_COLOR, EFFECT_BEGIN_COLOR);
                    Effect.AddPoint(tipPosition + new Vector2f(tipDimension.X + 1F, OutlineThickness), EFFECT_BEGIN_COLOR, EFFECT_END_COLOR);
                }
            }

            // top right
            for (UInt32 count = pointCount - pointCount / 4; count < pointCount; ++count)
            {
                float angle = count * 2 * (float)Math.PI / pointCount;
                Vector2f offset = new Vector2f((float)Math.Cos(angle), (float)Math.Sin(angle));

                Vector2f center = GetCenter(CornerType.TopRight);

                Background.AddPoint(center + offset * Radius, BackgroundColor, OutlineColor);
                Effect.AddPoint(center + offset * Radius, EFFECT_BEGIN_COLOR, EFFECT_END_COLOR);
                ShadowEffect.AddPoint(center + offset * Radius, SHADOW_EFFECT_COLOR, SHADOW_EFFECT_COLOR);
            }
            // bottom right
            for (UInt32 count = 0; count < pointCount / 4; ++count)
            {
                float angle = count * 2 * (float)Math.PI / pointCount;
                Vector2f offset = new Vector2f((float)Math.Cos(angle), (float)Math.Sin(angle));

                Vector2f center = GetCenter(CornerType.BottomRight);

                Background.AddPoint(center + offset * Radius, BackgroundColor, OutlineColor);
                Effect.AddPoint(center + offset * Radius, EFFECT_END_COLOR, EFFECT_END_COLOR);
                ShadowEffect.AddPoint(center + offset * Radius, SHADOW_EFFECT_COLOR, SHADOW_EFFECT_COLOR);
            }

            if (TipMode && TipBorderType != BorderType.Top)
            {
                Tip.AddPoint(tipPosition, BackgroundColor);
                Tip.AddPoint(tipPosition + new Vector2f(-tipDimension.X / 2F, tipDimension.Y), BackgroundColor);
                Tip.AddPoint(tipPosition - new Vector2f(tipDimension.X, 0F), BackgroundColor);

                EffectTip.AddPoint(tipPosition, EFFECT_END_COLOR);
                EffectTip.AddPoint(tipPosition + new Vector2f(-tipDimension.X / 2F, tipDimension.Y), EFFECT_END_COLOR);
                EffectTip.AddPoint(tipPosition - new Vector2f(tipDimension.X, 0F), EFFECT_END_COLOR);

                ShadowEffectTip.AddPoint(tipPosition, SHADOW_EFFECT_COLOR);
                ShadowEffectTip.AddPoint(tipPosition + new Vector2f(-tipDimension.X / 2F, tipDimension.Y), SHADOW_EFFECT_COLOR);
                ShadowEffectTip.AddPoint(tipPosition - new Vector2f(tipDimension.X, 0F), SHADOW_EFFECT_COLOR);

                if (TipMode)
                {
                    Background.AddPoint(tipPosition + new Vector2f(1F, -OutlineThickness), BackgroundColor, OutlineColor);
                    for (float count = 0; count < tipDimension.X; ++count)
                    {
                        Background.AddPoint(tipPosition - new Vector2f(count, OutlineThickness), BackgroundColor, BackgroundColor);
                    }
                    Background.AddPoint(tipPosition - new Vector2f(tipDimension.X + 1F, OutlineThickness), BackgroundColor, OutlineColor);

                    Effect.AddPoint(tipPosition + new Vector2f(1F, -OutlineThickness), EFFECT_END_COLOR, EFFECT_END_COLOR);
                    for (float count = 0; count < tipDimension.X; ++count)
                        Effect.AddPoint(tipPosition - new Vector2f(count, OutlineThickness), EFFECT_END_COLOR, EFFECT_END_COLOR);
                    Effect.AddPoint(tipPosition - new Vector2f(tipDimension.X + 1F, OutlineThickness), EFFECT_END_COLOR, EFFECT_END_COLOR);
                }
            }

            // bottom left
            for (UInt32 count = pointCount / 4; count < pointCount / 2; ++count)
            {
                float angle = count * 2 * (float)Math.PI / pointCount;
                Vector2f offset = new Vector2f((float)Math.Cos(angle), (float)Math.Sin(angle));

                Vector2f center = GetCenter(CornerType.BottomLeft);

                Background.AddPoint(center + offset * Radius, BackgroundColor, OutlineColor);
                Effect.AddPoint(center + offset * Radius, EFFECT_END_COLOR, EFFECT_END_COLOR);
                ShadowEffect.AddPoint(center + offset * Radius, SHADOW_EFFECT_COLOR, SHADOW_EFFECT_COLOR);
            }
        }

        public void SetPosition(Vector2f position, Boolean tipMode = true)
        {
            if (TipMode && tipMode)
                position = GetTipExtremityPosition();

            Position = Vector2I.FromVector2(position).ToVector2();

            Background.Position = Position;
            Tip.Position = Position;
            Effect.Position = Position;
            EffectTip.Position = Position;

            ShadowEffect.Position = Position + ShadowOffset;
            ShadowEffectTip.Position = Position + ShadowOffset;
        }

        public Vector2f GetTipExtremityPosition(Boolean local = false)
        {
            Vector2f offset = local ? Position : new Vector2f();

            switch (TipBorderType)
            {
                case BorderType.Top:
                    return
                        GetTipPosition() - offset +
                        new Vector2f(GetTipDimension().X / 2F, -GetTipDimension().Y);
                default:
                    return
                        GetTipPosition() - offset +
                        new Vector2f(-GetTipDimension().X / 2F, GetTipDimension().Y);  
            }
        }

        public void Draw(RenderTarget window)
        {
            if (ShadowMode)
                window.Draw(ShadowEffect);
            if (TipMode && ShadowMode)
                window.Draw(ShadowEffectTip);

            window.Draw(Effect);
            if (TipMode)
                window.Draw(EffectTip);

            window.Draw(Background);
            if (TipMode)
                window.Draw(Tip);
        }
    }
}
