using System;

namespace FF7Nostalgia.Core.Field
{
    public sealed class EncounterMeter
    {
        private readonly Random _random;
        private readonly float _minimumDistance;
        private readonly float _maximumDistance;
        private float _distanceUntilEncounter;

        public float DistanceUntilEncounter => _distanceUntilEncounter;

        public EncounterMeter(float minimumDistance = 18f, float maximumDistance = 42f, int? seed = null)
        {
            if (minimumDistance <= 0f) throw new ArgumentOutOfRangeException(nameof(minimumDistance));
            if (maximumDistance < minimumDistance) throw new ArgumentOutOfRangeException(nameof(maximumDistance));

            _minimumDistance = minimumDistance;
            _maximumDistance = maximumDistance;
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
            Reset();
        }

        public bool AddDistance(float distance)
        {
            if (distance <= 0f) return false;
            _distanceUntilEncounter -= distance;
            if (_distanceUntilEncounter > 0f) return false;
            Reset();
            return true;
        }

        public void Reset()
        {
            _distanceUntilEncounter = _minimumDistance +
                ((float)_random.NextDouble() * (_maximumDistance - _minimumDistance));
        }
    }
}
