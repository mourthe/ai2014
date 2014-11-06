// PrologC.h

#include <SWI-cpp.h>
#include <iostream>

namespace PrologC {

	class Prolog
	{
	public:
	
		static PlEngine initilize(char * path )
		{
			char* argv[] = {"swipl.dll", "-s", path , 0}; 
			_putenv("SWI_HOME_DIR=C:\\Program Files (x86)\\swipl"); 
 
			PlEngine eng(3,argv);
			return eng;
		}

		static int* bestMove( PlEngine e)
		{
			 PlTermv av(1); 
			
			 PlQuery q("bestMove", av); 

			 q.next_solution();
			 char * ret =  ( char*)av[0];
			int * res   = parseBestMove(ret);
			
			 return res;
 
		}
		
		static void updatePercp(PlEngine e , int x , int y , bool hasShine, bool hasCockroach, bool hasBreeze, bool hasDistortions, bool hasBinaries )
		{
			if(hasShine)
			{
				PlTermv av(2);
				av[0] = x ;
				av[1] = y ;
				PlCall("updPerShine", av);
			}
			
			if(hasCockroach){
				PlTermv av(2);
				av[0] = x ;
				av[1] = y ;
				PlCall("updPerCockS", av);
			}
			
			if(hasBreeze)
			{
				PlTermv av(2);
				av[0] = x ;
				av[1] = y ;
				PlCall("updBreeze", av);
			}

			if(hasDistortions)
			{
				PlTermv av(2);
				av[0] = x ;
				av[1] = y ;
				PlCall("updPerSpaceD", av);
			}

			if(!hasCockroach && !hasBreeze && !hasDistortions){
				PlTermv av(2);
				av[0] = x ;
				av[1] = y ;
				PlCall("setSafe", av);
			}

			if(hasBinaries){
				PlTermv av(2);
				av[0] = x ;
				av[1] = y ;
				PlCall("updPerBinaries", av);
			}
		
		}

		static bool isVisited(int x , int y )
		{
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("visited",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
		}


		static bool isSafe(int x, int y)
		{
			
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("safe",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
		}

		static void safes()
		{
			PlTermv av(2);

			PlQuery q("safe",av);

			System::Console::WriteLine("----safes----");
			while( q.next_solution() )
			{
				System::Console::WriteLine("{0},{1}",(int)av[0],(int)av[1]);
			}
			System::Console::WriteLine("----END safes----");
			
		}

		/********************/
		/* Assertions rules */
		/********************/
		static void putVortex(int x, int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("putVortex",av);
		}
		static void putAmmo(int x, int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("putAmmo",av);
		}
		static void putCock(int x,int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("putCock",av);
		}
		static void putBug(int x, int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("putBug",av);
		}

		static void putTerrain(int x, int y, int t)
		{
			PlTermv av(3);
			av[0] = x  ;
			av[1] = y ;
			av[2] = t ;
			PlCall("putTerrain",av);
		}
		
		static void updFacing(char * direction)
		{
			PlTermv av(1);
			av[0] = PlCompound(direction);
			PlCall("updFacing",av);
		}
		
		static void removeSafe(int x, int y)
		{
			PlTermv av(2);
			av[0] = x ;
			av[1] = y;
			PlCall("removeSafe",av);

		}

		/**************************/
		/* End - Assertions rules */
		/**************************/


		static void removeAmmo(int x, int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("rmvAmmo",av);
		}
		static void removeCockroach(int x,int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("rmvCockroach",av);
		}
		
		static bool isMart(int x, int y)
		{
			
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("mart",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
		}

		
		static bool isPokeCenter(int x, int y)
		{
			
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("pokeCenter",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
		}

		
		static bool isTrainer(int x, int y)
		{
			
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("trainer",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
		}

	private:


		static int * parseBestMove(char * answ)
		{
			char predicate[100] ; 
			char args[3][100] ; int argIx= 0;
			int * ret = (int*)malloc(sizeof(int)*5);
			int retIdx = 0;


			int i;
			// we must read to the first ( or '\0'
			for ( i = 0; !(answ[i] == '(' || answ[i] == '\0') ; i++)
			{
				predicate[i] = answ[i];
			}
			
			int j = 0;
			for (i ++ ; answ[i] != '\0'; i++)
			{
				if(answ[i] == ',' ||answ[i] == ')' )
				{
					args[argIx][j] = '\0';
					if(answ[i] == ')')
						break;
					else
					{						
						argIx++; j = 0 ;
					}
					
				}
				else
				{
					args[argIx][j] = answ[i];
					j++;
				}
			}

			// Predicate cases.
			switch (predicate[0])
			{
			case 'm': ret[retIdx] = Move ; break;
			case 't' :
				{
					BestMove turn ;
					if(predicate[4] == 'L')
						turn = TurnLeft;
					else
						turn = TurnRight;
					ret[retIdx] = turn ;   
					break;
				}
			case 'f' : ret[retIdx] = FixBug ;   break; 
			case 'a' : ret[retIdx] = Attack ;   break; 
			case 'd' : ret[retIdx] = Debug ; break;
			default:
				printf("Acao retornada pelo prolog desconhecida!"); exit(1);
				break;
			}
			retIdx++;

			for (int i = 0; i <= argIx; i++ , retIdx++)
			{
				int numArg = (int) strtol(args[i],NULL,0);
				if(numArg == 0 &&  strcmp( args[i] , "0" ) != 0  ) // then is a string as value, a pokemon!
					ret[retIdx] = -1;
				else
					ret[retIdx] = numArg;

			}
			
			ret[retIdx] = -1 ;
			
			return ret;
			

		}
	
    enum BestMove
    {
        Move,
        TurnRight,
        TurnLeft,
        AStar, 
        GetAmmo,
        Attack,
        FixBug,
        JumpVortex,
        Debug
    };

	};

}
