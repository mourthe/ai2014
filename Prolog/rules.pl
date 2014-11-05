%-----------------------------------
% Dynamic procedures
%-----------------------------------

:- dynamic vortex/2.
:- dynamic ammo/2.
:- dynamic hole/2.
:- dynamic trainer/2.
:- dynamic visited/2.
:- dynamic bug/2.
:- dynamic ammoShine/2.
:- dynamic spaceDistortions/2.
:- dynamic stinkCockroach/2.
:- dynamic breeze/2.
:- dynamic binariesFlying/2.
:- dynamic facing/1.
:- dynamic at/2.
:- dynamic visited/2. 
:- dynamic safeLst/1 .
:- dynamic cockroach/2.
:- dynamic terrainType/3.


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

% se nick está em X,Y, então este local foi visitado
visited(X,Y) :- at(X,Y) .

% se o nick esta em X,Y e não tem uma barata ali, ali é seguro.
safe(X,Y) :- safeLst(L) , isSafe(safe(X,Y),L) .

putBuilding(X,Y,T) :- assert((X,Y)) .
putCockroach(X,Y) :- not(cockroach(X,Y)) , assert(cockroach(X,Y)) , safeLst(L) , takeList(X,Y,L,L1).
putHole(X,Y) :-not(vortex(X,Y)) , assert(vortex(X,Y)) , safeLst(L) , takeList(X,Y,L,L1).
putVortex(X,Y) :- not(vortex(X,Y)) , assert(vortex(X,Y)) , safeLst(L) , takeList(X,Y,L,L1).
putTerrain(X,Y,T) :- assert(terrainType(X,Y,T)).

rmvCockroach(X,Y) :- retract(cockroach(X,Y)) , not(safe(X,Y)) , safeLst(L) , includeList(X,Y,L,L1).

removeSafe(X,Y) :- safeLst(L) , ( takeList(X,Y,L,L1) ) . 

% verifica se é terreno
iscomp(T) :-   not(T == 495).
allowed(X,Y) :- terrainType(X,Y,T) , iscomp(T) .


distance_min(L,MinXY ) :-  at(X,Y) , distance_min(L, X, Y, MinXY).
distance_min(L, X0, Y0, MinXY) :-    aggregate( min(D, [Xt,Yt]) , (member([Xt,Yt], L) , D is sqrt((Xt-X0)^2+(Yt-Y0)^2)), MinXY).


nrstCockroach(X,Y) :- setof([Xs,Ys], cockroach(Xs,Ys),L) , distance_min(L,MinXY) , MinXY = min(D,[X,Y]) .

nrstBug(X,Y) :- setof([Xs,Ys], bug(Xs,Ys),L) , distance_min(L,MinXY) , MinXY = min(D,[X,Y]) .

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

% Se x-1, x+1, y-1, y+1 tem distorções então x,y é vortex 
updVortex(X,Y) :- (inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , spaceDistortions(I,Y) , spaceDistortions(X,Iy) , spaceDistortions(D,Y) , spaceDistortions(X,Dy)), putVortex(X,Y) .

% Se x-1, x+1, y-1, y+1 tem brisa então x,y é buracos 
updHole(X,Y) :- (inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , breeze(I,Y) , breeze(X,Iy) , breeze(D,Y) , breeze(X,Dy)), putHole(X,Y) .

updCockroach(X,Y) :-  
(X==0 , inc(Y,Iy) , dec(Y,Dy) , stinkCockroach(1,Y) , stinkCockroach(X,Iy)  , stinkCockroach(X,Dy) , putCockroach(X,Y) ) ;
(X==41 , inc(Y,Iy) , dec(Y,Dy) , stinkCockroach(40,Y) , stinkCockroach(X,Iy)  , stinkCockroach(X,Dy) , putCockroach(X,Y) ) ;
(Y==0 , inc(X,I) , dec(X,D) , stinkCockroach(X,1) , stinkCockroach(I,Y)  , stinkCockroach(D,Y) , putCockroach(X,Y) ) ;
(Y==41 , inc(X,I) , dec(X,D) , stinkCockroach(X,40) , stinkCockroach(I,Y)  , stinkCockroach(D,Y) , putCockroach(X,Y) ) ;
(X==0 , Y=0 , inc(X,I) , inc(Y,Iy) , stinkCockroach(X,Iy) , stinkCockroach(I,Y) , putCockroach(X,Y) ) ;
(X==0 , Y=41 , inc(X,I) , dec(Y,D) , stinkCockroach(X,D) , stinkCockroach(I,Y) , putCockroach(X,Y) ) ; 
(X==41 , Y=0 , dec(X,D) , inc(Y,Iy) , stinkCockroach(X,Iy) , stinkCockroach(D,Y) , putCockroach(X,Y) ) ; 
(X==41 , Y=41 , dec(X,D) , dec(Y,Dy) , stinkCockroach(X,Dy) , stinkCockroach(D,Y) , putCockroach(X,Y) ) ;  
(inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , stinkCockroach(I,Y) , stinkCockroach(X,Iy)  , stinkCockroach(D,Y) , stinkCockroach(X,Dy) , putCockroach(X,Y)) .

tryCockroach(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , (updCockroach(I,Y);true) , (updCockroach(X,Iy);true) , (updCockroach(D,Y);true) , (updCockroach(X,Dy);true). 

tryVortex(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , (updVortex(I,Y);true) , (updVortex(X,Iy);true) , (updVortex(D,Y);true) , (updVortex(X,Dy);true).

tryHole(X,Y) :- inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , (updHole(I,Y);true) , (updHole(X,Iy);true) , (updHole(D,Y);true) , (updHole(X,Dy);true) .

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

updPerShine(X,Y) :-  not(ammo(X,Y)),  assert(ammo(X,Y)).
updPerSpaceD(X,Y) :- assert(spaceDistortions(X,Y)) , tryVortex(X,Y) .
updPerCockS(X,Y,COCKS) :- (COCKS == 1 , assert(stinkCockroach(X,Y)) , tryCockroach(X,Y)) ; (COCKS == 0 , setSafe(X,Y)) . 
updPerBinaries(X,Y,P) :- not(bug(X,Y)) , assert(bug(X,Y)).
updFacing(D) :- retract(facing(X)) , assert(facing(D)).
updBreeze(X,Y) :- assert(breeze(X,Y)), tryHole(X,Y).

%-----------------------------------
% End of perceptions
%-----------------------------------

%-----------------------------------
% Facts
%-----------------------------------

at(19,23).
visited(19,23).
facing(east).
safeLst([]).

%-----------------------------------
% End Facts
%-----------------------------------

%-----------------------------------
% Best moves
%-----------------------------------

inc(A, W) :- W is A + 1.
dec(B, K) :- K is B - 1.

bestMove(attack(X,Y)) :- 	stinkCockroach(X,Y) , ((at(X,Y), facing(north), dec(Y,D), cockroach(X,D), retract(cockroach(X,D))) ;
													(at(X,Y), facing(south), inc(Y,I), cockroach(X,I), retract(cockroach(X,I))) ;
													(at(X,Y), facing(west), dec(X,D), cockroach(D,Y), retract(cockroach(D,Y))) ; 
													(at(X,Y), facing(east), inc(X,I), cockroach(I,Y), retract(cockroach(I,Y)))).

bestMove(fixBug(X,Y)) :- at(X,Y) , bug(X,Y) , retract(bug(X,Y)).

bestMove(moveUp(D,Y)) :- (at(X,Y) , X > 0 , facing(north) , dec(X,D) , safe( D ,Y) , not(cockroach( D ,Y))  , not(visited(D,Y)) ,  allowed(D,Y) )
											, assert(at(D,Y)) , retract(at(X,Y)) , assert(visited(D,Y))  ,removeSafe(D,Y) .

bestMove(moveDown(I,Y)) :- (at(X,Y) , X < 41 , facing(south) , inc(X,I) , safe(I ,Y) , not(cockroach(I ,Y)) , not(visited(I,Y)) ,allowed(I,Y) ) 
											, assert(at(I,Y)) , retract(at(X,Y)) , assert(visited(I,Y)) ,removeSafe(I,Y) .

bestMove(moveRight(X,I)) :- (at(X,Y) , Y < 41 , facing(east) , inc(Y,I) , safe(X,I) , not(cockroach(X,I)) , not(visited(X,I)) ,  allowed(X,I) ) 
											, assert(at(X,I)) , retract(at(X,Y)) , assert(visited(X,I)) ,removeSafe(X,I)  .

bestMove(moveLeft(X,D)) :- (at(X,Y) , Y > 0 ,  facing(west) , dec(Y,D) , safe(X,D), not(cockroach(X,D))  , not(visited(X,D)) , allowed(X,D) ) 
											, assert(at(X,D)) , retract(at(X,Y)) , assert(visited(X,D)) ,removeSafe(X,D) .

bestMove(turnRight) :- 	(facing(north) , at(X,Y) , dec(X,D) , inc(Y,I) , (not(safe(D,Y)) ; not(allowed(D,Y)) ; visited(D,Y) )  , safe(X,I) , allowed(X,I)  , not(visited(X,I)) ,  assert(facing(east)) , retract(facing(north)) );
						(facing(south) , at(X,Y) , inc(X,I) , dec(Y,D) , (not(safe(I,Y)) ; not(allowed(I,Y)) ; visited(I,Y) )  , safe(X,D) , allowed(X,D)  , not(visited(X,D)) ,  assert(facing(west)) , retract(facing(south)) );
						(facing(east) , at(X,Y) , inc(Y,I) , inc(X,IX) , (not(safe(X,I)) ; not(allowed(X,I)) ; visited(X,I) )  , safe(IX,Y) ,allowed(IX,Y) , not(visited(IX,Y)) ,  assert(facing(south)) , retract(facing(east)) );
						(facing(west) , at(X,Y) , dec(Y,D) , dec(X,DX) , (not(safe(X,D)) ; not(allowed(X,D)) ; visited(X,D) )  , safe(DX,Y) ,allowed(DX,Y) , not(visited(DX,Y)) ,  assert(facing(north)) , retract(facing(west)) ).

bestMove(turnLeft) :- 	(facing(north) , at(X,Y) , dec(X,D) , dec(Y,DY) , (not(safe(D,Y)) ; not(allowed(D,Y)) ; visited(D,Y) )  , safe(X,DY) ,allowed(X,DY), not(visited(X,DY)) ,  assert(facing(west)) , retract(facing(north)) );
						(facing(south) , at(X,Y) , inc(X,I) , inc(Y,IY) , (not(safe(I,Y)) ; not(allowed(I,Y)) ; visited(I,Y) )  , safe(X,IY) ,allowed(X,IY), not(visited(X,IY)) ,  assert(facing(east)) , retract(facing(south)) );
						(facing(east) ,  at(X,Y) , inc(Y,I) , dec(X,D)  , (not(safe(X,I)) ; not(allowed(X,I)) ; visited(X,I) )  , safe(D,Y)  ,allowed(D,Y) , not(visited(D,Y)) ,  assert(facing(north)) , retract(facing(east)) );
						(facing(west) ,  at(X,Y) , dec(Y,D) , inc(X,I)  , (not(safe(X,D)) ; not(allowed(X,D)) ; visited(X,D) )  , safe(I,Y)  ,allowed(I,Y) , not(visited(I,Y)) ,  assert(facing(south)) , retract(facing(west)) ).

bestMove(moveUp(D,Y)) :- (at(X,Y) , X > 0 , facing(north) , dec(X,D)  , not(visited(D,Y)) ,  allowed(D,Y)  )  , assert(at(D,Y)) , retract(at(X,Y)) , assert(visited(D,Y)) .
bestMove(moveDown(I,Y)) :- (at(X,Y) , X < 41 , facing(south) , inc(X,I)  , not(visited(I,Y)) ,allowed(I,Y) ) ,  assert(at(I,Y)) , retract(at(X,Y)) , assert(visited(I,Y)) .
bestMove(moveRight(X,I)) :- (at(X,Y) , Y < 41  , facing(east) , inc(Y,I) , not(visited(X,I)) ,  allowed(X,I))  , assert(at(X,I)) , retract(at(X,Y)) , assert(visited(X,I)) .
bestMove(moveLeft(X,D)) :- (at(X,Y) , Y > 0 ,  facing(west) , dec(Y,D) , not(visited(X,D)) , allowed(X,D) ) , assert(at(X,D)) , retract(at(X,Y)) , assert(visited(X,D)) .

bestMove(debug(0,0)) .

%-----------------------------------
% End of Best moves
%-----------------------------------
