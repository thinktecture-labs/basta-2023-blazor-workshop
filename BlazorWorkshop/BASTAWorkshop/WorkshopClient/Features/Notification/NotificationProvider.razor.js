export async function allowNotification() {
    const result = await Notification.requestPermission();
    console.log(JSON.stringify(result));
    return result === 'granted';
}

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
