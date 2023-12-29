#include "Logic.mqh"

#property copyright COPYRIGHT
#property link      LINK
#property version   VERSION
#property icon      ICON
#property strict

int OnInit()
{
   return _OnInit();
}
void OnDeinit(const int reason)
{
   _OnDeinit(reason);
}
void OnTick()
{
   _OnTick(false);
}
void OnTimer()
{
   _OnTick(true);
}
void OnChartEvent(const int id,const long &lparam,const double &dparam,const string &sparam)
{
   _OnChartEvent(id,lparam,dparam,sparam);
}