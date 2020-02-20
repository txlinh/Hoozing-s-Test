       
        /// <summary>
        /// Write script in SQL Server
        /// </summary>
        SELECT TOP 1 T3.NAME ProductName, T.NumberOfSellingProducts
        FROM(
        SELECT T1.PRODUCTID , SUM(T1.QUANTITY) NumberOfSellingProducts
        FROM ORDER_DETAIL T1
        INNER JOIN ORDER T2 ON T1.ORDERID = T2.ID
        WHERE MONTH(T2.DATE) = (SELECT MONTH(MAX(T4.DATE)) FROM ORDER T4)
        GROUP BY  T1.PRODUCTID 
        ) T
        INNER JOIN PRODUCT T3 ON T.PRODUCTID = T3.ID
        ORDER BY T.NumberOfSellingProducts DESC