using System;

namespace Wallet
{
  public class Wallet
  {
    public double StartingAmount { get; private set; }
    public Wallet(double startingAmount)
    {
      this.StartingAmount = startingAmount;

    }
  }

}