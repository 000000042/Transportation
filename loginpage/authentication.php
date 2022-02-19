    <?php      
        include('connection.php');  
        $username = $_POST['username'];  
        $password = $_POST['pass'];  
		
   
            //to prevent from mysqli injection  
            $username = stripcslashes($username);  
            $password = stripcslashes($password);  
            $username = mysqli_real_escape_string($con, $username);  
            $password = mysqli_real_escape_string($con, $password);  
          
            $sql = "select *from login where username = '$username' and password = '$password'";  
            $result = mysqli_query($con, $sql);  
            $row = mysqli_fetch_array($result, MYSQLI_ASSOC);  
            $count = mysqli_num_rows($result);  
              
            if($count == 1){  
                echo "<h1><center> ورود شما در شرکت حمل و نقل سجاد موفقیت آمیز بود </center></h1>";
				echo "<h1><center><input type='submit' name='btn' value='انتقال به صفحه جدید'></h1></center>";
				
				header('Location: ..\usersection\pwlbhosseinbanaei\pwvs\zx\index.html');
				exit();
            }  
            else{  
                echo "<h1> تلاش شما برای اتصال به بخش خصوصی شرکت موفقت آمیز نبود ، دوباره تلاش کنید. </h1>";  
            }     
    ?>  