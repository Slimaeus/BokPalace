﻿namespace BokPalace.Domain.Interfaces;
public interface IEntity<out T> where T : notnull
{
    T Id { get; }
}
