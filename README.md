**Simple Promotion Engine for Checkout Cart:**

This program is intended to do with .Net based with C# language where user able to apply different promotion at checkout process.  

**Problem Statement:**

 You need to implement a simple promotion engine for a checkout process. Cart contains a list of single character SKU ids (A, B, C.	) over which the promotion engine will need to run.
 
The promotion engine will need to calculate the total order value after applying the 2 promotion types:

•	buy 'n' items of a SKU for a fixed price (3 A's for 130)
•	buy SKU 1 & SKU 2 for a fixed price ( C + D = 30 )

**Test Setup**
Unit price for SKU IDs A	50
B	30
C	20
D	15

**Active Promotions:**

3 of A's for 130
2 of B's for 45 C & D for 30

**Scenario A**

1	* A	50

1	* B	30

1	* C	20

**Total:100**		

**Scenario	B	**

5 * A		130 + 2*50

5 * B		45 + 45 + 30

1 * C		28

**Total	:370**

**Scenario C**

3	* A	130

5	* B	45 + 45 + 1 * 30

1	* C	-

1	* D	30

**Total:280**


**Build, Run and Test Instructions:**

Source code is implemended by Visual studio 2017 and .Net 2.0 framework. 
  
 Unit test run by Xunit Framework. 

**Running the Program:**

Open the solution by using visual studio 2017 and make sure Promotion Engine console is set as a startup project. 



**Running the Tests:**


**Notes for Design, Implementation:**


**Notes on Tests:**

**Further Necessary Improvements:**
