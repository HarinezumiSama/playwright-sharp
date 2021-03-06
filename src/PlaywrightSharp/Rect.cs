using System;

namespace PlaywrightSharp
{
    /// <summary>
    /// Bounding box data returned by <see cref="IElementHandle.GetBoundingBoxAsync"/>.
    /// </summary>
    public class Rect : IEquatable<Rect>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rect"/> class.
        /// </summary>
        public Rect()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rect"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public Rect(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// The x coordinate of the element in pixels.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The y coordinate of the element in pixels.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The width of the element in pixels.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// The height of the element in pixels.
        /// </summary>
        public double Height { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((Rect)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Rect"/> is equal to the current <see cref="Rect"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Rect"/> to compare with the current <see cref="Rect"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Rect"/> is equal to the current
        /// <see cref="Rect"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Rect obj)
            => obj != null &&
                obj.X == X &&
                obj.Y == Y &&
                obj.Height == Height &&
                obj.Width == Width;

        /// <inheritdoc/>
        public override int GetHashCode()
            => X.GetHashCode() * 397
                ^ Y.GetHashCode() * 397
                ^ Width.GetHashCode() * 397
                ^ Height.GetHashCode() * 397;
    }
}
