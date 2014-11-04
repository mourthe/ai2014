#include "stdafx.h"
#include "PrologC.h"


#pragma once
using namespace System;
namespace ManagedProlog {
	public ref class Prolog
	{
	public:
		Prolog(){}


		static void Initilize(char * path)
		{
			PlEngine e = PrologC::Prolog::initilize(path);
			eng = (void*) &e;
		}
		
		static int * BestMove()
		{
			PlEngine * e = (PlEngine *) eng;
			return  PrologC::Prolog::bestMove( *e ) ;
		}

		static void UpdPerc(int x , int y , bool hasShine, bool hasCockroach, bool hasBreeze, bool hasDistortions, bool hasBinaries)
		{
			PlEngine * e = (PlEngine *) eng;
			PrologC::Prolog::updatePercp(*e,x,y, hasShine, hasCockroach, hasBreeze, hasDistortions, hasBinaries);
		}

		static bool IsVisited(int x , int y )
		{
			return PrologC::Prolog::isVisited(x,y);
		}

		static bool IsSafe(int x, int y)
		{
			return PrologC::Prolog::isSafe(x,y);
		}

		static bool IsMart(int x, int y)
		{
			return PrologC::Prolog::isMart(x,y);
		}

		static void PutTerrain(int x, int y, int t)
		{
			PrologC::Prolog::putTerrain(x,y,t);
		}

		
		static bool IsPokeCenter(int x, int y)
		{
			return PrologC::Prolog::isPokeCenter(x,y);
		}

		
		static bool IsTrainer(int x, int y)
		{
			return PrologC::Prolog::isTrainer(x,y);
		}

		static void UpdFacing(char * direction)
		{
			PrologC::Prolog::updFacing(direction);
		}

		static void RemoveSafe(int x,int y)
		{
			PrologC::Prolog::removeSafe(x,y);
		}
		
		static void Safes()
		{
			PrologC::Prolog::safes();
		}

	private:
		static void * eng;	

	};
}
