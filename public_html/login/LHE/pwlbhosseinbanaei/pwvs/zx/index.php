
<html xmlns="http://www.w3.org/1999/xhtml">
<META charset="utf-8">
<meta http-equiv="x-ua-compatible" content="IE=edge">
<meta name="viewport" content="width=device-widgh, intial-scale=1.0">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>صفحه امنیتی شرکت سجاد ساری</title>
    <script type="text/javascript">
        var s = "";

        while (s != "mypassword") {
            s = prompt("گذرواژه اختصاصی شرکت  سجاد را وارد کنید");
            if (s == "mypassword") {
                window.location.href = "http://localhost/sajadtransport/public_html/login/LHE"; //page to redirect if password entered is correct

            } else {
                alert("گذرواژه شما اشتباه میباشد - شما به پل ارتباطی منتقل میشوید");
                window.location.href = "http://localhost/sajadtransport/landingpagge/";


            }
        }
    </script>
</head>

<body>
</body>

</html>