export async function showConferenceNotification(title, message) {
    console.log('show notification');
    if (Notification.permission === 'granted') {
        new Notification(title, {
            body: message
        });
    }
    else {
        if (Notification.permission !== 'denied') {
            const permission = await Notification.requestPermission();

            if (permission === 'granted') {
                new Notification(title, {
                    body: message
                });
            }
        }
    }
}
