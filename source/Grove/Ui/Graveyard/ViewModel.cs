﻿namespace Grove.Ui.Graveyard
{
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Linq;
  using Caliburn.Micro;
  using Core;
  using Infrastructure;

  public class ViewModel
  {
    private readonly SelectableCard.ViewModel.IFactory _cardVmFactory;

    private readonly BindableCollection<SelectableCard.ViewModel> _cards =
      new BindableCollection<SelectableCard.ViewModel>();

    public ViewModel(IEnumerable<Card> graveyard, SelectableCard.ViewModel.IFactory cardVmFactory)
    {
      _cardVmFactory = cardVmFactory;
      _cards.AddRange(graveyard.Select(cardVmFactory.Create));

      ((INotifyCollectionChanged) graveyard).CollectionChanged += Synchronize;
    }


    public IEnumerable<SelectableCard.ViewModel> Cards { get { return _cards; } }

    private void Synchronize(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        _cards.Clear();
      }

      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        foreach (Card card in e.NewItems)
        {
          _cards.Add(_cardVmFactory.Create(card));
        }
      }

      if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        foreach (Card card in e.OldItems)
        {
          var viewModel = _cards.Single(x => x.Card == card);

          _cards.Remove(viewModel);
          viewModel.Close();
        }
      }
    }


    public interface IFactory
    {
      ViewModel Create(IEnumerable<Card> graveyard);
    }
  }
}