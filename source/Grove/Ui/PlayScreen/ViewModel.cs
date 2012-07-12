﻿namespace Grove.Ui.PlayScreen
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using Core;
  using Core.Messages;
  using Core.Testing;
  using Infrastructure;
  using Shell;

  public class ViewModel : IIsDialogHost, IReceive<PlayerHasCastASpell>, IReceive<PlayerHasActivatedAbility>,
    IReceive<PlayerHasCycledCard>, IReceive<SearchStarted>, IReceive<SearchFinished>, IReceive<DamageHasBeenDealt>,
    IReceive<AssignedCombatDamageWasDealt>
  {
    private readonly List<object> _largeDialogs = new List<object>();
    private readonly List<object> _notifications = new List<object>();
    private readonly ScenarioGenerator _scenarioGenerator;
    private readonly IShell _shell;

    private readonly List<object> _smallDialogs = new List<object>();

    public ViewModel(IShell shell, Players players, Battlefield.ViewModel.IFactory battlefieldFactory,
                     ScenarioGenerator scenarioGenerator)
    {
      _shell = shell;
      _scenarioGenerator = scenarioGenerator;

      OpponentsBattlefield = battlefieldFactory.Create(players.Computer);
      YourBattlefield = battlefieldFactory.Create(players.Human);
    }

    public object LargeDialog { get { return _largeDialogs.FirstOrDefault(); } }
    public MagnifiedCard.ViewModel MagnifiedCard { get; set; }
    public ManaPool.ViewModel ManaPool { get; set; }
    public virtual object Notification { get { return _notifications.FirstOrDefault(); } }
    public Battlefield.ViewModel OpponentsBattlefield { get; private set; }
    public Ui.Players.ViewModel PlayersBox { get; set; }
    public virtual bool SearchInProgress { get; set; }
    public object SmallDialog { get { return _smallDialogs.FirstOrDefault(); } }
    public Stack.ViewModel Stack { get; set; }
    public Turn.ViewModel Turn { get; set; }
    public Battlefield.ViewModel YourBattlefield { get; private set; }
    public Zones.ViewModel Zones { get; set; }

    [Updates("SmallDialog", "Notification", "LargeDialog")]
    public virtual void AddDialog(object dialog, DialogType dialogType)
    {
      switch (dialogType)
      {
        case (DialogType.Small):
          {
            _smallDialogs.Insert(0, dialog);
            break;
          }

        case (DialogType.Large):
          {
            _largeDialogs.Insert(0, dialog);
            break;
          }

        case (DialogType.Notification):
          {
            _notifications.Insert(0, dialog);
            break;
          }
      }
    }

    public bool HasFocus(object dialog)
    {
      if (LargeDialog != null)
      {
        return dialog == LargeDialog;
      }

      return dialog == SmallDialog;
    }

    [Updates("SmallDialog", "Notification", "LargeDialog")]
    public virtual void RemoveDialog(object dialog)
    {
      _smallDialogs.Remove(dialog);
      _notifications.Remove(dialog);
      _largeDialogs.Remove(dialog);
    }

    public void Receive(AssignedCombatDamageWasDealt message)
    {
      // pause the game a bit after dealing combat damage
      // before creatures go to graveyards
      Thread.Sleep(500);
    }

    public void Receive(DamageHasBeenDealt message)
    {
      _shell.ShowNotification(message.ToString());
    }

    public void Receive(PlayerHasActivatedAbility message)
    {
      _shell.ShowNotification(message.ToString());
    }


    public void Receive(PlayerHasCastASpell message)
    {
      _shell.ShowNotification(message.ToString());
    }

    public void Receive(PlayerHasCycledCard message)
    {
      _shell.ShowNotification(message.ToString());
    }

    public void Receive(SearchFinished message)
    {
      SearchInProgress = false;
    }

    public void Receive(SearchStarted message)
    {
      SearchInProgress = true;
    }

    public void GenerateTestScenario()
    {
      _scenarioGenerator.WriteScenario();
    }

    public interface IFactory
    {
      ViewModel Create();
    }
  }
}