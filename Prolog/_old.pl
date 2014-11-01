%-----------------------------------
% Dynamic procedures
%-----------------------------------

:- dynamic mart/2.
:- dynamic pokeCenter/2.
:- dynamic trainer/2.
:- dynamic visited/2.
:- dynamic pokemon/3.
:- dynamic perfumeJoy/2.
:- dynamic screamSeller/2.
:- dynamic screamTrainer/2.
:- dynamic facing/1.
:- dynamic at/2.
:- dynamic visited/2. 
:- dynamic hurtPokemon/0.
:- dynamic pokeball/1.
:- dynamic safeLst/1 .
:- dynamic pokedex/1.

%-----------------------------------
% End of dynamic procedures
%-----------------------------------

%-----------------------------------
% Lists
%-----------------------------------


addList(X,L,[X|L]).

delList(X,[X|Tail],Tail).
delList(X,[Y|Tail],[Y|Tail1]) :- delList(X,Tail,Tail1).

isPart(X,[X|Tail]).
isPart(X,[Head|Tail]) :- isPart(X,Tail).

removeHead([Head|Tail],Head,Tail).

% rem(V,[V|Tail],Tail) , V=safe(X,Y) , allowed(X,Y).
% rem(V,[Q|Tail],[Q|Tail1]) :- rem(V,Tail,Tail1).

remove(X,Y,[L|LS],L1) :- removeHead([L|LS],L,Tail) , L = safe(X,Y) , allowed(X,Y).

isSafe(H,[H|R]) :- H = safe(X,Y) .
isSafe(H,[Y|R]) :- isSafe(H,R).

isEmpty([]).

%-----------------------------------
% End of Lists
%-----------------------------------

%-----------------------------------
% Some rules
%-----------------------------------

% se o local é safe e ainda nao foi visitado coloca na lista
includeList(X,Y,L,L1) :- (not(visited(X,Y)) , (X > -1  , X < 42) , (Y > -1 , Y < 42 ) , addList(safe(X,Y),L,L1) , retract(safeLst(L)) , assert(safeLst(L1)) ) ; true .

% se o local acabou de ser visitado tira da lista
takeList(X,Y,L,L1) :- delList(safe(X,Y),L,L1) , retract(safeLst(L)) , assert(safeLst(L1)) .


% se ash está em X,Y, então este local foi visitado
% visited(X,Y) :- at(X,Y) .

% se o ash esta em X,Y e não tem trainador ali, ali é seguro.
safe(X,Y) :- safeLst(L) , isSafe(safe(X,Y),L) .


putBuilding(X,Y,T) :- assert(groundType(X,Y,T)) .
putAmmo(X,Y) :- not(ammo(X,Y)) , assert(ammo(X,Y)).
putCockroach(X,Y) :- not(cockroach(X,Y)) , assert(cockroach(X,Y)) , safeLst(L) , takeList(X,Y,L,L1).
rmvAmmo(X,Y) :- retract(ammo(X,Y)).
rmvCockroach(X,Y) :- retract(cockroach(X,Y)) , not(safe(X,Y)) , safeLst(L) , includeList(X,Y,L,L1).

removeSafe(X,Y) :- safeLst(L) , ( takeList(X,Y,L,L1) ) . 


% verifica se é terreno
iscomp(G) :-   (G == 71) ;

			   (G == 76 , fire)  .

allowed(X,Y) :- groundType(X,Y,G) , iscomp(G) .

setType(P) :- ( type(P,T) , type(P,K) , T \== K , assert(T) , assert(K) ) ; (type(P,K) , assert(K) ).


distance_min(L,MinXY ) :-  at(X,Y) , distance_min(L, X, Y, MinXY).
distance_min(L, X0, Y0, MinXY) :-    aggregate( min(D, [Xt,Yt]) , (member([Xt,Yt], L) , D is sqrt((Xt-X0)^2+(Yt-Y0)^2)), MinXY).

nrstPokeCenter(X,Y) :- setof([Xs,Ys], pokeCenter(Xs,Ys),L) , distance_min(L,MinXY) , MinXY = min(D,[X,Y]) .

nrstTrainer(X,Y) :- setof([Xs,Ys], trainer(Xs,Ys),L) , distance_min(L,MinXY) , MinXY = min(D,[X,Y]) .

nrstPokemon(X,Y) :- setof([Xs,Ys], pokemon(Xs,Ys),L) , distance_min(L,MinXY) , MinXY = min(D,[X,Y]) .

pokemon(X,Y) :- pokemon(X,Y,_).


nrstAllowed(L,MinXY ) :-  at(X,Y) , nrstAllowed(L, X, Y, MinXY).
nrstAllowed(L, X0, Y0, MinXY) :-    aggregate( min(D, safe(Xt,Yt)) , (member(safe(Xt,Yt), L) , D is sqrt((Xt-X0)^2+(Yt-Y0)^2) , allowed(Xt,Yt) ), MinXY).

isAllowed(H,L) :- nrstAllowed(L,MinSfAlw) , MinSfAlw = min(D,H) .


%-----------------------------------
% End of some rules
%-----------------------------------

border(X,Y) :- X == 0 ; X == 41 ; Y == 0 ; Y == 41.

%-----------------------------------
% Perceptions
%-----------------------------------

updPokeCenter(X,Y) :- (inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , perfumeJoy(I,Y) , perfumeJoy(X,Iy) , perfumeJoy(D,Y) , perfumeJoy(X,Dy)), putPokeCenter(X,Y) . 

updMart(X,Y) :- (inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , screamSeller(I,Y) , screamSeller(X,Iy) , screamSeller(D,Y) , screamSeller(X,Dy)), putMart(X,Y) .

updTrainer(X,Y) :-  
(X==0 , inc(Y,Iy) , dec(Y,Dy) , screamTrainer(1,Y) , screamTrainer(X,Iy)  , screamTrainer(X,Dy) , putTrainer(X,Y) ) ;
(X==41 , inc(Y,Iy) , dec(Y,Dy) , screamTrainer(40,Y) , screamTrainer(X,Iy)  , screamTrainer(X,Dy) , putTrainer(X,Y) ) ;
(Y==0 , inc(X,I) , dec(X,D) , screamTrainer(X,1) , screamTrainer(I,Y)  , screamTrainer(D,Y) , putTrainer(X,Y) ) ;
(Y==41 , inc(X,I) , dec(X,D) , screamTrainer(X,40) , screamTrainer(I,Y)  , screamTrainer(D,Y) , putTrainer(X,Y) ) ;
(X==0 , Y=0 , inc(X,I) , inc(Y,Iy) , screamTrainer(X,Iy) , screamTrainer(I,Y) , putTrainer(X,Y) ) ;
(X==0 , Y=41 , inc(X,I) , dec(Y,D) , screamTrainer(X,D) , screamTrainer(I,Y) , putTrainer(X,Y) ) ; 
(X==41 , Y=0 , dec(X,D) , inc(Y,Iy) , screamTrainer(X,Iy) , screamTrainer(D,Y) , putTrainer(X,Y) ) ; 
(X==41 , Y=41 , dec(X,D) , dec(Y,Dy) , screamTrainer(X,Dy) , screamTrainer(D,Y) , putTrainer(X,Y) ) ;  
(inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , screamTrainer(I,Y) , screamTrainer(X,Iy)  , screamTrainer(D,Y) , screamTrainer(X,Dy) , putTrainer(X,Y)) .

tryPokeCenter(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , (updPokeCenter(I,Y);true) , (updPokeCenter(X,Iy);true) , (updPokeCenter(D,Y);true) , (updPokeCenter(X,Dy);true) .
tryTrainer(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , (updTrainer(I,Y);true) , (updTrainer(X,Iy);true) , (updTrainer(D,Y);true) , (updTrainer(X,Dy);true). 

trySeller(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , (updMart(I,Y);true) , (updMart(X,Iy);true) , (updMart(D,Y);true) , (updMart(X,Dy);true).
setSafe(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , safeLst(L) ,(

				(( not(visited(I,Y)) , not(safe(I,Y)) ), ( not(visited(X,Iy)) , not(safe(X,Iy)) ) , ( not(visited(D,Y)) , not(safe(D,Y)) ) , ( not(visited(X,Dy)) , not(safe(X,Dy)))   , 
					includeList(I,Y,L,L1)  , includeList(X,Iy,L1,L2)   , includeList(D,Y,L2,L3) ,   includeList(X,Dy,L3,L4) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ), ( not(visited(X,Iy)) , not(safe(X,Iy)) ) , ( not(visited(D,Y)) , not(safe(D,Y)) ) ,(visited(X,Dy) ; safe(X,Dy) ) 	    , 
					includeList(I,Y,L,L1) ,   includeList(X,Iy,L1,L2)   , includeList(D,Y,L2,L3) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ), ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(visited(D,Y) ; safe(D,Y) )  , ( not(visited(X,Dy)) , not(safe(X,Dy)) )  , 
					includeList(I,Y,L,L1) ,   includeList(X,Iy,L1,L2)  , includeList(X,Dy,L2,L3) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ), ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(visited(D,Y) ; safe(D,Y) )  	  ,(visited(X,Dy) ; safe(X,Dy) )       , 
					includeList(I,Y,L,L1)  , includeList(X,Iy,L1,L2) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ),(visited(X,Iy) ; safe(X,Iy) ) 	   ,( not(visited(D,Y)) , not(safe(D,Y)) ) , ( not(visited(X,Dy)) , not(safe(X,Dy)) )  , 
					includeList(I,Y,L,L1)   , includeList(D,Y,L1,L3) ,   includeList(X,Dy,L3,L4) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ),(visited(X,Iy) ; safe(X,Iy) ) 	   ,( not(visited(D,Y)) , not(safe(D,Y)) ) ,(visited(X,Dy) ; safe(X,Dy) )       , 
					includeList(I,Y,L,L1)   , includeList(D,Y,L1,L3)  ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ),( visited(X,Iy) ; safe(X,Iy) ) 	   ,(visited(D,Y) ; safe(D,Y) ) 	    , (not(visited(X,Dy)),not(safe(X,Dy)) )   , 
					includeList(I,Y,L,L1) ,  includeList(X,Dy,L1,L4) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ),(visited(X,Iy) ; safe(X,Iy) ) 	   ,(visited(D,Y) ; safe(D,Y) ) 	  ,(visited(X,Dy) ; safe(X,Dy) )       , 
					includeList(I,Y,L,L1)  ) ;

				((visited(I,Y) ; safe(I,Y) )	  , ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(not(visited(D,Y)) , not(safe(D,Y)) ) , (not(visited(X,Dy)) , not(safe(X,Dy)) ),   
					includeList(X,Iy,L,L2)   , includeList(D,Y,L2,L3)   , includeList(X,Dy,L3,L4) ) ;

				((visited(I,Y) ; safe(I,Y) )	  , ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(not(visited(D,Y)) , not(safe(D,Y)) ) ,(visited(X,Dy) ; safe(X,Dy) )     , 
					includeList(X,Iy,L,L2)   , includeList(D,Y,L2,L3) ) ;

				((visited(I,Y) ; safe(I,Y) )	  , ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(visited(D,Y) ; safe(D,Y) ) 	  , (not(visited(X,Dy)) , not(safe(X,Dy)) ),  
					includeList(X,Iy,L,L3)  , includeList(X,Dy,L3,L4) ) ;

				((visited(I,Y) ; safe(I,Y) )	  , ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(visited(D,Y) ; safe(D,Y) ) 	  ,(visited(X,Dy) ; safe(X,Dy) )       ,
					includeList(X,Iy,L,L3)  ) ;

				((visited(I,Y) ; safe(I,Y) )	  ,(visited(X,Iy) ; safe(X,Iy) ) 	   ,( not(visited(D,Y)) ,not(safe(D,Y)) ) , (not(visited(X,Dy)) , not(safe(X,Dy))  ) , 
					includeList(D,Y,L,L3)  , includeList(X,Dy,L3,L4) ) ;

				((visited(I,Y) ; safe(I,Y) )	  ,(visited(X,Iy) ; safe(X,Iy) ) 	   ,( not(visited(D,Y)) ,not(safe(D,Y)) ) ,(visited(X,Dy) ; safe(X,Dy) )       , 
					includeList(D,Y,L,L3)  ) ;

				((visited(I,Y) ; safe(I,Y) )	  ,(visited(X,Iy) ; safe(X,Iy) ) 	   ,(visited(D,Y) ; safe(D,Y) ) 	  , (not(visited(X,Dy)),not(safe(X,Dy))) , 
					includeList(X,Dy,L,L4) ) )  .

updPerfum(X,Y) :-  assert(perfumeJoy(X,Y)) , tryPokeCenter(X,Y) .
updPerScremS(X,Y) :- assert(screamSeller(X,Y)) , trySeller(X,Y) .
updPerScremT(X,Y,SCREAMT) :- (SCREAMT == 1 , assert(screamTrainer(X,Y)) , tryTrainer(X,Y)) ; (SCREAMT == 0 , setSafe(X,Y)) . 
updPokemon(X,Y,P) :- not(pokemon(X,Y,P)) , assert(pokemon(X,Y,P)) .
updFacing(D) :- retract(facing(X)) , assert(facing(D)) .


%-----------------------------------
% End of perceptions
%-----------------------------------

%-----------------------------------
% Facts
%-----------------------------------

at(19,23).
visited(19,23).
facing(west).
safeLst([]).

%-----------------------------------
% End Facts
%-----------------------------------

%-----------------------------------
% Best moves
%-----------------------------------

inc(A, W) :- W is A + 1.
dec(B, K) :- K is B - 1.

bestMove(healPokemon(X,Y)) :- at(X,Y) , pokeCenter(X,Y) ,  hurtPokemon , retract(hurtPokemon) .
bestMove(battleTrainer(X,Y,R)) :- at(X,Y), trainer(X,Y) , ( ( hurtPokemon , R = 0 ) ; ( not(hurtPokemon) , R = 1 , assert(hurtPokemon) , retract(trainer(X,Y)) ) ) .

bestMove(launchPokeball(P)) :- at(X,Y) , pokemon(X,Y,P), pokeball(N) , (  N > 0  , retract(pokemon(X,Y,P)) , dec(N,ND) , retract(pokeball(N)) , assert(pokeball(ND)) , setType(P) , pokedex(PN) , inc(PN,IPN) , retract(pokedex(PN)) , assert(pokedex(IPN)) ) .
bestMove(catchPokemon(Xg,Yg)) :- nrstPokemon(X,Y), pokemon(X,Y,P) , visited(X,Y) ,Xg = X , Yg = Y  ,pokeball(N) , N >  0 , retract(at(H,J)) , assert(at(X,Y)) .

bestMove(moveUp(D,Y)) :- (at(X,Y) , X > 0 , facing(north) , dec(X,D) , safe( D ,Y) , not(trainer( D ,Y))  , not(visited(D,Y)) ,  allowed(D,Y) )
											, assert(at(D,Y)) , retract(at(X,Y)) , assert(visited(D,Y))  ,removeSafe(D,Y) .

bestMove(moveDown(I,Y)) :- (at(X,Y) , X < 41 , facing(south) , inc(X,I) , safe(I ,Y) , not(trainer(I ,Y)) , not(visited(I,Y)) ,allowed(I,Y) ) 
											, assert(at(I,Y)) , retract(at(X,Y)) , assert(visited(I,Y)) ,removeSafe(I,Y) .

bestMove(moveRight(X,I)) :- (at(X,Y) , Y < 41 , facing(east) , inc(Y,I) , safe(X,I) , not(trainer(X,I)) , not(visited(X,I)) ,  allowed(X,I) ) 
											, assert(at(X,I)) , retract(at(X,Y)) , assert(visited(X,I)) ,removeSafe(X,I)  .

bestMove(moveLeft(X,D)) :- (at(X,Y) , Y > 0 ,  facing(west) , dec(Y,D) , safe(X,D), not(trainer(X,D))  , not(visited(X,D)) , allowed(X,D) ) 
											, assert(at(X,D)) , retract(at(X,Y)) , assert(visited(X,D)) ,removeSafe(X,D) .

bestMove(turnRight) :- 	(facing(north) , at(X,Y) , dec(X,D) , inc(Y,I) , (not(safe(D,Y)) ; not(allowed(D,Y)) ; visited(D,Y) )  , safe(X,I) , allowed(X,I)  , not(visited(X,I)) ,  assert(facing(east)) , retract(facing(north)) );
						(facing(south) , at(X,Y) , inc(X,I) , dec(Y,D) , (not(safe(I,Y)) ; not(allowed(I,Y)) ; visited(I,Y) )  , safe(X,D) , allowed(X,D)  , not(visited(X,D)) ,  assert(facing(west)) , retract(facing(south)) );
						(facing(east) , at(X,Y) , inc(Y,I) , inc(X,IX) , (not(safe(X,I)) ; not(allowed(X,I)) ; visited(X,I) )  , safe(IX,Y) ,allowed(IX,Y) , not(visited(IX,Y)) ,  assert(facing(south)) , retract(facing(east)) );
						(facing(west) , at(X,Y) , dec(Y,D) , dec(X,DX) , (not(safe(X,D)) ; not(allowed(X,D)) ; visited(X,D) )  , safe(DX,Y) ,allowed(DX,Y) , not(visited(DX,Y)) ,  assert(facing(north)) , retract(facing(west)) ).

bestMove(turnLeft) :- 	(facing(north) , at(X,Y) , dec(X,D) , dec(Y,DY) , (not(safe(D,Y)) ; not(allowed(D,Y)) ; visited(D,Y) )  , safe(X,DY) ,allowed(X,DY), not(visited(X,DY)) ,  assert(facing(west)) , retract(facing(north)) );
						(facing(south) , at(X,Y) , inc(X,I) , inc(Y,IY) , (not(safe(I,Y)) ; not(allowed(I,Y)) ; visited(I,Y) )  , safe(X,IY) ,allowed(X,IY), not(visited(X,IY)) ,  assert(facing(east)) , retract(facing(south)) );
						(facing(east) ,  at(X,Y) , inc(Y,I) , dec(X,D)  , (not(safe(X,I)) ; not(allowed(X,I)) ; visited(X,I) )  , safe(D,Y)  ,allowed(D,Y) , not(visited(D,Y)) ,  assert(facing(north)) , retract(facing(east)) );
						(facing(west) ,  at(X,Y) , dec(Y,D) , inc(X,I)  , (not(safe(X,D)) ; not(allowed(X,D)) ; visited(X,D) )  , safe(I,Y)  ,allowed(I,Y) , not(visited(I,Y)) ,  assert(facing(south)) , retract(facing(west)) ).


bestMove(moveUp(D,Y)) :- (at(X,Y) , X > 0 , facing(north) , dec(X,D)  , not(visited(D,Y)) ,  allowed(D,Y)  , not(hurtPokemon) ) , assert(at(D,Y)) , retract(at(X,Y)) , assert(visited(D,Y)) .
bestMove(moveDown(I,Y)) :- (at(X,Y) , X < 41 , facing(south) , inc(X,I)  , not(visited(I,Y)) ,allowed(I,Y) , not(hurtPokemon) ),  assert(at(I,Y)) , retract(at(X,Y)) , assert(visited(I,Y)) .
bestMove(moveRight(X,I)) :- (at(X,Y) , Y < 41  , facing(east) , inc(Y,I) , not(visited(X,I)) ,  allowed(X,I) , not(hurtPokemon) ) , assert(at(X,I)) , retract(at(X,Y)) , assert(visited(X,I)) .
bestMove(moveLeft(X,D)) :- (at(X,Y) , Y > 0 ,  facing(west) , dec(Y,D) , not(visited(X,D)) , allowed(X,D) , not(hurtPokemon) ) , assert(at(X,D)) , retract(at(X,Y)) , assert(visited(X,D)) .


bestMove(aStar(Xg,Yg)) :- safeLst(L) , not(isEmpty(L)) , isAllowed(H,L) , H = safe(Xg,Yg) , ( takeList(Xg,Yg,L,LR) ) , retract(at(X,Y)) , assert(at(Xg,Yg)) , assert(visited(Xg,Yg)) .

bestMove(joker(0,0)) .

%-----------------------------------
% End of Best moves
%-----------------------------------
