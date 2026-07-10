using FactoryMethodExample;

INotification email = NotificationFactory.Create("Email");
email.Send("Your order has been placed.");

INotification sms = NotificationFactory.Create("SMS");
sms.Send("Your OTP is 1234.");
