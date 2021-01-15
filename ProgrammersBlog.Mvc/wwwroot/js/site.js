
function convertFirstLetterToUpperCase(text) //yeni eklenen kayıtlarda true false değerinin ilk harfi küçük o yüzden bu fonksiyon yazıldı
{
    return text.charAt(0).toUpperCase()+ text.slice(1);
}
function convertToShortDate(dateString) {
    const shortDate = new Date(dateString).toLocaleDateString('en-US');
    return shortDate;
}