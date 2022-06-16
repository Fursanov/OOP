from . import views
from django.urls import path

urlpatterns = [
    path('$', views.cart_detail, name='cart_detail'),
    path('add/(?P<post_id>\d+)/$', views.cart_add, name='cart_add'),
    path('remove/(?P<post_id>\d+)/$', views.cart_remove, name='cart_remove'),
]