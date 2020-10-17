# calculator
Evaluate Excel-like expressions including cell references and ranges

Based on the work from Jonathan Wood, 2010

http://www.blackbeltcoder.com/Articles/algorithms/a-c-expression-evaluator

---

Can process complex arithmetic:

* 1 + 1
* round(pi * (abs(pow(-3, 2)) + sqrt(147 * (14 + 27))))

Can evaluate cell references and cell ranges, from A1 to ZZZ999:

* 1 + A1
* sum(A1, A2, C6)
* average(B1:B10)

Recognizes System.Math symbols:

* PI
* E

Recognizes System.Math functions:

* abs(double)
* acos(double)
* asin(double)
* atan(double)
* atan2(double, double)
* ceiling(double)
* cos(double)
* cosh(double)
* exp(double)
* floor(double)
* log(double)
* log10(double)
* max(double, double)
* min(double, double)
* pow(double, double)
* round(double)
* sign(double)
* sin(double)
* sinh(double)
* sqrt(double)
* tan(double)
* tanh(double)
* trunc(double)

Recognizes extended functions that take multiple parameters:

* average(params double[])
* max(params double[])
* median(params double[])
* min(params double[])
* mode(params double[])
* range(params double[])
* stdev(params double[])
* sum(params double[])
* variance(params double[])

