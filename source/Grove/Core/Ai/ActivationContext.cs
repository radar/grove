﻿namespace Grove.Core.Ai
{
  using System.Collections.Generic;
  using System.Linq;
  using Targeting;

  public class ActivationContext
  {
    private readonly List<TargetsCombination> _targetsWithX = new List<TargetsCombination>();
    public bool CanCancel = true;
    public bool CancelActivation;
    public Card Card { get; private set; }
    public int? MaxX { get; private set; }
    public TargetSelector Selector { get; private set; }
    public int? X;
    public int DistributeAmount { get; private set; }

    public ActivationContext() {}

    public ActivationContext(ActivationPrerequisites prerequisites)
    {
      Card = prerequisites.Card;      
      MaxX = prerequisites.MaxX;
      Selector = prerequisites.Selector;
      DistributeAmount = prerequisites.DistributeAmount;
    }

    public bool HasTargets { get { return _targetsWithX.Count > 0; } }

    public void SetPossibleTargets(IEnumerable<Targets> targetsCombinations)
    {
      _targetsWithX.AddRange(targetsCombinations.Select(x => new TargetsCombination {Targets = x}));
    }

    public IEnumerable<TargetsCombination> TargetsCombinations()
    {
      return _targetsWithX;
    }

    public void RemoveTargetCombination(int index)
    {
      _targetsWithX.RemoveAt(index);
    }

    public class TargetsCombination
    {
      public Targets Targets;
      public int? X;
    }
  }
}