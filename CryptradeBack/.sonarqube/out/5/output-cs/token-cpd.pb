Þ;
g/Users/artemivliev/Artem/Cryptrade/CryptradeBack/PortfolioManagement/Controllers/PortfolioController.cs
	namespace 	
PortfolioManagement
 
. 
Controllers )
{		 
[

 
ApiController

 
]

 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
PortfolioController $
:% &
ControllerBase' 5
{ 
private 	
readonly
 
PortfolioService #
_portfolioService$ 5
;5 6
public 
PortfolioController	 
( 
PortfolioService -
portfolioService. >
)> ?
{ 
_portfolioService 
= 
portfolioService '
;' (
} 
[ 	
HttpGet	 
] 
[ 	
	Authorize	 
] 
public 
IActionResult 
GetPortfolioById -
(- .
). /
{ 	
try 
{ 
var 
userId 
= 
User !
.! "
	FindFirst" +
(+ ,

ClaimTypes, 6
.6 7
NameIdentifier7 E
)E F
?F G
.G H
ValueH M
;M N
var 
	portfolio 
= 
_portfolioService  1
.1 2
GetPortfolioById2 B
(B C
ConvertC J
.J K
ToInt32K R
(R S
userIdS Y
)Y Z
)Z [
;[ \
if 
( 
	portfolio 
!=  
null! %
)% &
{ 
return   
Ok   
(   
	portfolio   '
)  ' (
;  ( )
}!! 
else!! 
{"" 
return## 
NotFound## #
(### $
$str##$ >
)##> ?
;##? @
}$$ 
}%% 
catch&& 
(&& 
	Exception&& 
ex&& 
)&&  
{'' 
return(( 

BadRequest(( !
(((! "
$"((" $
$str(($ 9
{((9 :
ex((: <
.((< =
Message((= D
}((D E
"((E F
)((F G
;((G H
})) 
}** 	
[,, 	
HttpPost,,	 
],, 
[-- 	
	Authorize--	 
]-- 
public.. 
IActionResult.. 
CreatePortfolio.. ,
(.., -
[..- .
FromBody... 6
]..6 7
PortfolioDataModel..8 J
model..K P
)..P Q
{// 	
try00 
{11 
var22 
modelUserId22 
=22  !
User22" &
.22& '
	FindFirst22' 0
(220 1

ClaimTypes221 ;
.22; <
NameIdentifier22< J
)22J K
?22K L
.22L M
Value22M R
;22R S
_portfolioService33 !
.33! "
CreatePortfolio33" 1
(331 2
model332 7
,337 8
Convert339 @
.33@ A
ToInt3233A H
(33H I
modelUserId33I T
)33T U
)33U V
;33V W
return44 
Ok44 
(44 
$str44 /
)44/ 0
;440 1
}55 
catch66 
(66 
	Exception66 
ex66 
)66  
{77 
return88 

BadRequest88 !
(88! "
$"88" $
$str88$ 9
{889 :
ex88: <
.88< =
Message88= D
}88D E
"88E F
)88F G
;88G H
}99 
}:: 	
[<< 	
HttpPut<<	 
]<< 
[== 	
	Authorize==	 
]== 
public>> 
IActionResult>> 
UpdatePortfolio>> ,
(>>, -
int>>- 0
portfolioId>>1 <
,>>< =
[>>> ?
FromBody>>? G
]>>G H
PortfolioDataModel>>I [
model>>\ a
)>>a b
{?? 	
try@@ 
{AA 
_portfolioServiceBB !
.BB! "
UpdatePortfolioBB" 1
(BB1 2
portfolioIdBB2 =
,BB= >
modelBB? D
.BBD E
nameBBE I
,BBI J
modelBBK P
.BBP Q
descriptionBBQ \
)BB\ ]
;BB] ^
returnCC 
OkCC 
(CC 
$strCC ?
)CC? @
;CC@ A
}DD 
catchEE 
(EE 
	ExceptionEE 
exEE 
)EE  
{FF 
returnGG 

BadRequestGG !
(GG! "
$"GG" $
$strGG$ E
{GGE F
exGGF H
.GGH I
MessageGGI P
}GGP Q
"GGQ R
)GGR S
;GGS T
}HH 
}II 	
[KK 	

HttpDeleteKK	 
]KK 
[LL 	
	AuthorizeLL	 
]LL 
publicMM 
IActionResultMM 
DeletePortfolioMM ,
(MM, -
intMM- 0
portfolioIdMM1 <
)MM< =
{NN 	
tryOO 
{PP 
varQQ 
userIdQQ 
=QQ 
UserQQ !
.QQ! "
	FindFirstQQ" +
(QQ+ ,

ClaimTypesQQ, 6
.QQ6 7
NameIdentifierQQ7 E
)QQE F
?QQF G
.QQG H
ValueQQH M
;QQM N
_portfolioServiceRR !
.RR! "
DeletePortfolioRR" 1
(RR1 2
portfolioIdRR2 =
,RR= >
ConvertRR? F
.RRF G
ToInt32RRG N
(RRN O
userIdRRO U
)RRU V
)RRV W
;RRW X
returnSS 
OkSS 
(SS 
$strSS ;
)SS; <
;SS< =
}TT 
catchUU 
(UU 
	ExceptionUU 
exUU 
)UU  
{VV 
returnWW 

BadRequestWW !
(WW! "
$"WW" $
$strWW$ @
{WW@ A
exWWA C
.WWC D
MessageWWD K
}WWK L
"WWL M
)WWM N
;WWN O
}XX 
}YY 	
[[[ 	
HttpGet[[	 
([[ 
$str[[ 
)[[ 
][[  
public\\ 
async\\ 
Task\\ 
<\\ 
IActionResult\\ '
>\\' (
GetTotalValue\\) 6
(\\6 7
int\\7 :
portfolioId\\; F
)\\F G
{]] 	
try^^ 
{__ 
var`` 
portfolioData`` !
=``" #
await``$ )
_portfolioService``* ;
.``; <
GetTotalValue``< I
(``I J
portfolioId``J U
)``U V
;``V W
ifbb 
(bb 
portfolioDatabb !
!=bb" $
nullbb% )
)bb) *
{cc 
returndd 
Okdd 
(dd 
portfolioDatadd +
)dd+ ,
;dd, -
}ee 
elseff 
{gg 
returnhh 
NotFoundhh #
(hh# $
)hh$ %
;hh% &
}ii 
}jj 
catchkk 
(kk 
	Exceptionkk 
exkk 
)kk  
{ll 
returnmm 

BadRequestmm !
(mm! "
$"mm" $
$strmm$ 9
{mm9 :
exmm: <
.mm< =
Messagemm= D
}mmD E
"mmE F
)mmF G
;mmG H
}nn 
}oo 	
}pp 
}qq †
_/Users/artemivliev/Artem/Cryptrade/CryptradeBack/PortfolioManagement/Data/PortfolioDbContext.cs
	namespace 	
PortfolioManagement
 
. 
Data "
{ 
public		 
class		 
PortfolioDbContext		  
:		! "
	DbContext		# ,
{

 
public 
PortfolioDbContext !
(! "
DbContextOptions" 2
<2 3
PortfolioDbContext3 E
>E F
optionsG N
)N O
:P Q
baseR V
(V W
optionsW ^
)^ _
{ 	
try 
{ 
var 
databaseCreator #
=$ %
Database& .
.. /

GetService/ 9
<9 :
IDatabaseCreator: J
>J K
(K L
)L M
asN P%
RelationalDatabaseCreatorQ j
;j k
if 
( 
databaseCreator #
!=$ &
null' +
)+ ,
{ 
if 
( 
! 
databaseCreator (
.( )

CanConnect) 3
(3 4
)4 5
)5 6
databaseCreator7 F
.F G
CreateG M
(M N
)N O
;O P
if 
( 
! 
databaseCreator (
.( )
	HasTables) 2
(2 3
)3 4
)4 5
databaseCreator6 E
.E F
CreateTablesF R
(R S
)S T
;T U
} 
} 
catch 
( 
	Exception 
ex 
)  
{ 
throw 
new 
	Exception #
(# $
ex$ &
.& '
ToString' /
(/ 0
)0 1
)1 2
;2 3
} 
} 	
public 
DbSet 
< 
	Portfolio 
> 

Portfolios  *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
} 
} ¢
X/Users/artemivliev/Artem/Cryptrade/CryptradeBack/PortfolioManagement/Entity/Portfolio.cs
	namespace 	
PortfolioManagement
 
. 
Entity $
{ 
[ 
Table 

(
 
$str 
) 
] 
public 
class 
	Portfolio 
{		 
[

 	
Key

	 
]

 
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
id 
{ 
get 
; 
set  
;  !
}" #
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Column	 
( 
$str 
) 
] 
public 
string 
description !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Column	 
( 
$str 
) 
] 
public 
double 

totalValue  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
Column	 
( 
$str 
) 
] 
public 
int 
userId 
{ 
get 
;  
set! $
;$ %
}& '
} 
} ø
o/Users/artemivliev/Artem/Cryptrade/CryptradeBack/PortfolioManagement/Migrations/20231109235525_InitialCreate.cs
	namespace 	
PortfolioManagement
 
. 

Migrations (
{ 
public		 

partial		 
class		 
InitialCreate		 &
:		' (
	Migration		) 2
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
AlterDatabase *
(* +
)+ ,
. 

Annotation 
( 
$str +
,+ ,
$str- 6
)6 7
;7 8
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str "
," #
columns 
: 
table 
=> !
new" %
{ 
id 
= 
table 
. 
Column %
<% &
int& )
>) *
(* +
type+ /
:/ 0
$str1 6
,6 7
nullable8 @
:@ A
falseB G
)G H
. 

Annotation #
(# $
$str$ C
,C D(
MySqlValueGenerationStrategyE a
.a b
IdentityColumnb p
)p q
,q r
name 
= 
table  
.  !
Column! '
<' (
string( .
>. /
(/ 0
type0 4
:4 5
$str6 @
,@ A
nullableB J
:J K
falseL Q
)Q R
. 

Annotation #
(# $
$str$ 3
,3 4
$str5 >
)> ?
,? @
description 
=  !
table" '
.' (
Column( .
<. /
string/ 5
>5 6
(6 7
type7 ;
:; <
$str= G
,G H
nullableI Q
:Q R
falseS X
)X Y
. 

Annotation #
(# $
$str$ 3
,3 4
$str5 >
)> ?
,? @

totalValue 
=  
table! &
.& '
Column' -
<- .
double. 4
>4 5
(5 6
type6 :
:: ;
$str< D
,D E
nullableF N
:N O
falseP U
)U V
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 4
,4 5
x6 7
=>8 :
x; <
.< =
id= ?
)? @
;@ A
}   
)   
.!! 

Annotation!! 
(!! 
$str!! +
,!!+ ,
$str!!- 6
)!!6 7
;!!7 8
}"" 	
	protected%% 
override%% 
void%% 
Down%%  $
(%%$ %
MigrationBuilder%%% 5
migrationBuilder%%6 F
)%%F G
{&& 	
migrationBuilder'' 
.'' 
	DropTable'' &
(''& '
name(( 
:(( 
$str(( "
)((" #
;((# $
})) 	
}** 
}++ ¢	
a/Users/artemivliev/Artem/Cryptrade/CryptradeBack/PortfolioManagement/Models/PortfolioDataModel.cs
	namespace 	
PortfolioManagement
 
. 
Models $
{ 
public 
class 
PortfolioDataModel  
{ 
[ 
Required 
] 
public		 
int			 
userId		 
{		 
get		 
;		 
set		 
;		 
}		  !
[

 
Required

 
]

 
public 
string	 
name 
{ 
get 
; 
set 
;  
}! "
public 
string	 
description 
{ 
get !
;! "
set# &
;& '
}( )
public 
double	 

totalValue 
{ 
get  
;  !
set" %
;% &
}' (
public 
int	 
id 
{ 
get 
; 
set 
; 
} 
} 
} 
^/Users/artemivliev/Artem/Cryptrade/CryptradeBack/PortfolioManagement/Models/TotalValueModel.cs
	namespace 	
PortfolioManagement
 
. 
Models $
{ 
public 
class 
TotalValueModel 
{ 
public 
double	 

totalValue 
{ 
get  
;  !
set" %
;% &
}' (
public 
double	 

profitLoss 
{ 
get  
;  !
set" %
;% &
}' (
public 
int	 
portfolioId 
{ 
get 
; 
set  #
;# $
}% &
}		 
}

 ³)
O/Users/artemivliev/Artem/Cryptrade/CryptradeBack/PortfolioManagement/Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder

 
.

 
Services

 
.

 
AddControllers

 
(

  
)

  !
;

! "
builder 
. 
Services 
. 
	AddScoped 
< 
PortfolioService +
>+ ,
(, -
)- .
;. /
builder 
. 
Services 
. 
	AddScoped 
< 
RabbitMQConsumer +
>+ ,
(, -
)- .
;. /
builder 
. 
Services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddSwaggerGen 
( 
)  
;  !
builder 
. 
Services 
. &
AddCustomJwtAuthentication +
(+ ,
), -
;- .
var 
dbHost 

= 
Environment 
. "
GetEnvironmentVariable /
(/ 0
$str0 9
)9 :
;: ;
var 
dbName 

= 
Environment 
. "
GetEnvironmentVariable /
(/ 0
$str0 9
)9 :
;: ;
var 

dbPassword 
= 
Environment 
. "
GetEnvironmentVariable 3
(3 4
$str4 F
)F G
;G H
var 
connectionString 
= 
$" 
$str  
{  !
dbHost! '
}' (
$str( <
{< =
dbName= C
}C D
$strD X
{X Y

dbPasswordY c
}c d
"d e
;e f
builder 
. 
Services 
. 
AddDbContext 
< 
PortfolioDbContext 0
>0 1
(1 2
options2 9
=>: <
options 
. 
UseMySql 
( 
connectionString %
,% &
new' *
MySqlServerVersion+ =
(= >
new> A
VersionB I
(I J
$numJ K
,K L
$numM N
,N O
$numP Q
)Q R
)R S
)S T
)T U
;U V
builder 
. 
Services 
. 
AddCors 
( 
options  
=>! #
{ 
options 
. 
	AddPolicy 
( 
$str *
,* +
builder   
=>   
builder   
.!! 
WithOrigins!! 
(!! 
$str!! 0
)!!0 1
."" 
AllowAnyHeader"" 
("" 
)"" 
.## 
AllowAnyMethod## 
(## 
)## 
)## 
;## 
}$$ 
)$$ 
;$$ 
builder&& 
.&& 
Services&& 
.&& 
AddCors&& 
(&& 
options&&  
=>&&! #
{'' 
options(( 
.(( 
	AddPolicy(( 
((( 
$str(( *
,((* +
builder)) 
=>)) 
builder)) 
.** 
WithOrigins** 
(** 
$str** 0
)**0 1
.++ 
AllowAnyHeader++ 
(++ 
)++ 
.,, 
AllowAnyMethod,, 
(,, 
),, 
),, 
;,, 
}-- 
)-- 
;-- 
var// 
app// 
=// 	
builder//
 
.// 
Build// 
(// 
)// 
;// 
app22 
.22 
UseCors22 
(22 
$str22  
)22  !
;22! "
app33 
.33 
UseCors33 
(33 
$str33  
)33  !
;33! "
if66 
(66 
app66 
.66 
Environment66 
.66 
IsDevelopment66 !
(66! "
)66" #
)66# $
{77 
app88 
.88 

UseSwagger88 
(88 
)88 
;88 
app99 
.99 
UseSwaggerUI99 
(99 
)99 
;99 
}:: 
app<< 
.<< 
UseHttpsRedirection<< 
(<< 
)<< 
;<< 
app>> 
.>> 
UseAuthentication>> 
(>> 
)>> 
;>> 
app?? 
.?? 
UseAuthorization?? 
(?? 
)?? 
;?? 
appAA 
.AA 
MapControllersAA 
(AA 
)AA 
;AA 
appCC 
.CC 
RunCC 
(CC 
)CC 	
;CC	 
Ú8
a/Users/artemivliev/Artem/Cryptrade/CryptradeBack/PortfolioManagement/Services/PortfolioService.cs
	namespace 	
PortfolioManagement
 
. 
Services &
{ 
public		 
class		 
PortfolioService		 
{

 
private 
readonly 
PortfolioDbContext +
_context, 4
;4 5
private 
readonly 
RabbitMQConsumer )
_rabbitMQConsumer* ;
;; <
private 

Dictionary 
< 
int 
, 
double  &
>& '!
_portfolioTotalValues( =
=> ?
new@ C

DictionaryD N
<N O
intO R
,R S
doubleT Z
>Z [
([ \
)\ ]
;] ^
public 
PortfolioService 
(  
PortfolioDbContext  2
context3 :
,: ;
RabbitMQConsumer< L
rabbitMQConsumerM ]
)] ^
{ 	
_rabbitMQConsumer 
= 
rabbitMQConsumer  0
;0 1
_context 
= 
context 
; 
} 	
public 
PortfolioDataModel !
GetPortfolioById" 2
(2 3
int3 6
userId7 =
)= >
{ 	
var 
	portfolio 
= 
_context $
.$ %

Portfolios% /
./ 0
FirstOrDefault0 >
(> ?
p? @
=>A C
pD E
.E F
userIdF L
==M O
userIdP V
)V W
;W X
if 
( 
	portfolio 
!= 
null !
)! "
{ 
var 
portfolioData !
=" #
new$ '
PortfolioDataModel( :
{ 
name 
= 
	portfolio $
.$ %
name% )
,) *
description 
=  !
	portfolio" +
.+ ,
description, 7
,7 8

totalValue 
=  
	portfolio! *
.* +

totalValue+ 5
,5 6
userId   
=   
	portfolio   &
.  & '
userId  ' -
,  - .
id!! 
=!! 
	portfolio!! "
.!!" #
id!!# %
}"" 
;"" 
return$$ 
portfolioData$$ $
;$$$ %
}%% 
return'' 
null'' 
;'' 
}(( 	
public** 
void** 
CreatePortfolio** #
(**# $
PortfolioDataModel**$ 6
model**7 <
,**< =
int**> A
modelUserId**B M
)**M N
{++ 	
var,, 
newPortfolio,, 
=,, 
new,, "
	Portfolio,,# ,
{-- 
name.. 
=.. 
model.. 
... 
name.. !
,..! "
description// 
=// 
model// #
.//# $
description//$ /
,/// 0
userId00 
=00 
modelUserId00 $
,00$ %

totalValue11 
=11 
model11 "
.11" #

totalValue11# -
}22 
;22 
_context44 
.44 

Portfolios44 
.44  
Add44  #
(44# $
newPortfolio44$ 0
)440 1
;441 2
_context55 
.55 
SaveChanges55  
(55  !
)55! "
;55" #
}66 	
public88 
void88 
UpdatePortfolio88 #
(88# $
int88$ '
portfolioId88( 3
,883 4
string885 ;
newName88< C
,88C D
string88E K
newDescription88L Z
)88Z [
{99 	
var:: 
	portfolio:: 
=:: 
_context:: $
.::$ %

Portfolios::% /
.::/ 0
Find::0 4
(::4 5
portfolioId::5 @
)::@ A
;::A B
if<< 
(<< 
	portfolio<< 
!=<< 
null<< !
)<<! "
{== 
	portfolio>> 
.>> 
name>> 
=>>  
newName>>! (
;>>( )
	portfolio?? 
.?? 
description?? %
=??& '
newDescription??( 6
;??6 7
_context@@ 
.@@ 
SaveChanges@@ $
(@@$ %
)@@% &
;@@& '
}AA 
}BB 	
publicDD 
asyncDD 
TaskDD 
<DD 
TotalValueModelDD )
>DD) *
GetTotalValueDD+ 8
(DD8 9
intDD9 <
portfolioIdDD= H
)DDH I
{EE 	
tryFF 
{GG 
_rabbitMQConsumerHH !
.HH! "
StartConsumingHH" 0
(HH0 1
)HH1 2
;HH2 3
varII 

totalValueII 
=II  
awaitII! &
_rabbitMQConsumerII' 8
.II8 9&
GetTotalValueByPortfolioIdII9 S
(IIS T
portfolioIdIIT _
)II_ `
;II` a
varJJ 

profitLossJJ 
=JJ  
awaitJJ! &
_rabbitMQConsumerJJ' 8
.JJ8 9&
GetProfitLossByPortfolioIdJJ9 S
(JJS T
portfolioIdJJT _
)JJ_ `
;JJ` a
varKK 
portfolioDataKK !
=KK" #
newKK$ '
TotalValueModelKK( 7
{LL 

totalValueMM 
=MM  

totalValueMM! +
,MM+ ,

profitLossNN 
=NN  

profitLossNN! +
,NN+ ,
portfolioIdOO 
=OO  !
portfolioIdOO" -
}PP 
;PP 
returnQQ 
portfolioDataQQ $
;QQ$ %
}RR 
catchSS 
(SS 
	ExceptionSS 
exSS 
)SS  
{TT 
throwUU 
exUU 
;UU 
}VV 
}WW 	
publicYY 
voidYY 
DeletePortfolioYY #
(YY# $
intYY$ '
portfolioIdYY( 3
,YY3 4
intYY5 8
userIdYY9 ?
)YY? @
{ZZ 	
var[[ 
	portfolio[[ 
=[[ 
_context[[ $
.[[$ %

Portfolios[[% /
.[[/ 0
FirstOrDefault[[0 >
([[> ?
p[[? @
=>[[A C
p[[D E
.[[E F
id[[F H
==[[I K
portfolioId[[L W
&&[[X Z
p[[[ \
.[[\ ]
userId[[] c
==[[d f
userId[[g m
)[[m n
;[[n o
if\\ 
(\\ 
	portfolio\\ 
!=\\ 
null\\ !
)\\! "
{]] 
_context^^ 
.^^ 

Portfolios^^ #
.^^# $
Remove^^$ *
(^^* +
	portfolio^^+ 4
)^^4 5
;^^5 6
_context__ 
.__ 
SaveChanges__ $
(__$ %
)__% &
;__& '
}`` 
}aa 	
}bb 	
}cc »>
a/Users/artemivliev/Artem/Cryptrade/CryptradeBack/PortfolioManagement/Services/RabbitMQConsumer.cs
	namespace

 	
PortfolioManagement


 
.

 
Services

 &
{ 
public 

class 
RabbitMQConsumer !
:" #
IDisposable$ /
{ 
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
private 
IConnection 
_connection '
;' (
private 
IModel 
_channel 
;  
private 
readonly 
object 
_lock  %
=& '
new( +
object, 2
(2 3
)3 4
;4 5
private 
bool 
_isConsuming !
=" #
false$ )
;) *
private 
TotalValueModel 
storedTotalValue  0
;0 1
private  
TaskCompletionSource $
<$ %
bool% )
>) *!
_consumptionCompleted+ @
=A B
newC F 
TaskCompletionSourceG [
<[ \
bool\ `
>` a
(a b
)b c
;c d
public 
RabbitMQConsumer 
(  
IConfiguration  .
configuration/ <
)< =
{ 	
_configuration 
= 
configuration *
;* +
InitializeRabbitMQ 
( 
)  
;  !
} 	
private 
void 
InitializeRabbitMQ '
(' (
)( )
{ 	
var 
factory 
= 
new 
ConnectionFactory /
{   
HostName!! 
=!! 
_configuration!! )
[!!) *
$str!!* =
]!!= >
,!!> ?
UserName"" 
="" 
_configuration"" )
["") *
$str""* =
]""= >
,""> ?
Password## 
=## 
_configuration## )
[##) *
$str##* =
]##= >
}$$ 
;$$ 
_connection&& 
=&& 
factory&& !
.&&! "
CreateConnection&&" 2
(&&2 3
)&&3 4
;&&4 5
_channel'' 
='' 
_connection'' "
.''" #
CreateModel''# .
(''. /
)''/ 0
;''0 1
_channel** 
.** 
ExchangeDeclare** $
(**$ %
_configuration**% 3
[**3 4
$str**4 K
]**K L
,**L M
ExchangeType**N Z
.**Z [
Fanout**[ a
)**a b
;**b c
_channel++ 
.++ 
QueueDeclare++ !
(++! "
_configuration++" 0
[++0 1
$str++1 E
]++E F
,++F G
durable++H O
:++O P
true++Q U
,++U V
	exclusive++W `
:++` a
false++b g
,++g h

autoDelete++i s
:++s t
false++u z
,++z {
	arguments	++| …
:
++… †
null
++‡ ‹
)
++‹ Œ
;
++Œ 
_channel,, 
.,, 
	QueueBind,, 
(,, 
_configuration,, -
[,,- .
$str,,. B
],,B C
,,,C D
_configuration,,E S
[,,S T
$str,,T k
],,k l
,,,l m
$str,,n p
),,p q
;,,q r
}-- 	
public// 
void// 
StartConsuming// "
(//" #
)//# $
{00 	
if11 
(11 
_isConsuming11 
)11 
{22 
return33 
;33 
}44 
var66 
consumer66 
=66 
new66 !
EventingBasicConsumer66 4
(664 5
_channel665 =
)66= >
;66> ?
consumer77 
.77 
Received77 
+=77  
(77! "
model77" '
,77' (
ea77) +
)77+ ,
=>77- /
{88 
try99 
{:: 
var;; 
body;; 
=;; 
ea;; !
.;;! "
Body;;" &
.;;& '
ToArray;;' .
(;;. /
);;/ 0
;;;0 1
var<< 
message<< 
=<<  !
Encoding<<" *
.<<* +
UTF8<<+ /
.<</ 0
	GetString<<0 9
(<<9 :
body<<: >
)<<> ?
;<<? @
var>> 

totalValue>> "
=>># $
JsonConvert>>% 0
.>>0 1
DeserializeObject>>1 B
<>>B C
TotalValueModel>>C R
>>>R S
(>>S T
message>>T [
)>>[ \
;>>\ ]
lock@@ 
(@@ 
_lock@@ 
)@@  
{AA 
storedTotalValueBB (
=BB) *

totalValueBB+ 5
;BB5 6
}CC !
_consumptionCompletedFF )
.FF) *
TrySetResultFF* 6
(FF6 7
trueFF7 ;
)FF; <
;FF< =
}GG 
catchHH 
(HH 
	ExceptionHH  
exHH! #
)HH# $
{II 
ConsoleKK 
.KK 
	WriteLineKK %
(KK% &
$"KK& (
$strKK( G
{KKG H
exKKH J
}KKJ K
"KKK L
)KKL M
;KKM N
}LL 
}MM 
;MM 
_channelOO 
.OO 
BasicConsumeOO !
(OO! "
queueOO" '
:OO' (
_configurationOO) 7
[OO7 8
$strOO8 L
]OOL M
,OOM N
autoAckOOO V
:OOV W
trueOOX \
,OO\ ]
consumerOO^ f
:OOf g
consumerOOh p
)OOp q
;OOq r
_isConsumingPP 
=PP 
truePP 
;PP  
}QQ 	
publicSS 
asyncSS 
TaskSS 
<SS 
doubleSS  
>SS  !&
GetTotalValueByPortfolioIdSS" <
(SS< =
intSS= @
portfolioIdSSA L
)SSL M
{TT 	
awaitVV !
_consumptionCompletedVV '
.VV' (
TaskVV( ,
;VV, -
lockXX 
(XX 
_lockXX 
)XX 
{YY 
ifZZ 
(ZZ 
storedTotalValueZZ $
.ZZ$ %
portfolioIdZZ% 0
==ZZ1 3
portfolioIdZZ4 ?
)ZZ? @
{[[ 
return\\ 
storedTotalValue\\ +
.\\+ ,

totalValue\\, 6
;\\6 7
}]] 
else^^ 
return^^ 
$num^^ 
;^^ 
}__ 
}`` 	
publicbb 
asyncbb 
Taskbb 
<bb 
doublebb  
>bb  !&
GetProfitLossByPortfolioIdbb" <
(bb< =
intbb= @
portfolioIdbbA L
)bbL M
{cc 	
awaitee !
_consumptionCompletedee '
.ee' (
Taskee( ,
;ee, -
lockgg 
(gg 
_lockgg 
)gg 
{hh 
ifii 
(ii 
storedTotalValueii $
.ii$ %
portfolioIdii% 0
==ii1 3
portfolioIdii4 ?
)ii? @
{jj 
returnkk 
storedTotalValuekk +
.kk+ ,

profitLosskk, 6
;kk6 7
}ll 
elsemm 
returnmm 
$nummm 
;mm 
}nn 
}oo 	
publicqq 
voidqq 
Disposeqq 
(qq 
)qq 
{rr 	
_channelss 
.ss 
Disposess 
(ss 
)ss 
;ss 
_connectiontt 
.tt 
Disposett 
(tt  
)tt  !
;tt! "
}uu 	
}vv 
}ww 