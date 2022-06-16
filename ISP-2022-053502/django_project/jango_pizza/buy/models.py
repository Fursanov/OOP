from django.db import models

class buy(models.Model):
    usr = models.ForeignKey(
        'auth.User',
        on_delete=models.CASCADE,
    )
    image = models.ImageField(upload_to='images/')
    cost = models.CharField(max_length=200)
 
    def __str__(self):
        return self.title