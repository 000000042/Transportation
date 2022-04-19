<?php
    session_start();
    unset($_SESSION["loggedinuser"]);
    session_destroy();
?>
<!DOCTYPE html>
    <html>

        <head>
            <title>خروح از پنل کاربری</title>
                <meta http-equiv="refresh" content="7; url =http://localhost/sajadtransport/public_html/" />
        </head>

      <body>
            <h1 style="text-align:center;color:green;">
                شما با موفقیت از سیستم  خارج شدید
            </h1>
                <p style="text-align:center;">
                        شما در چند ثانیه آینده از سیستم کاربری خود خارج خواهید شد 
                        (شرکت حمل و نقل سجاد ساری)
                </p>

        </body>

    </html>
<?php
