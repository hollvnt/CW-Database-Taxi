use teeest;

CREATE UNIQUE INDEX UserIndex
ON register (id_user, login_user, password_user, driver_status);