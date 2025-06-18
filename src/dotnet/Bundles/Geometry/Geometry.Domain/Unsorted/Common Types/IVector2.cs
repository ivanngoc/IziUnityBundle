using System;


namespace IziHardGames.Libs.NonEngine.Vectors
{
    public interface IVector2<T> where T : IEquatable<T>, IComparable<T>
	{
		public T X { get; set; }
		public T Y { get; set; }
	}
}