import {Modal} from "@/shared/ui/modal";
import styles from "./AuthModal.module.css"
import {Button} from "@/shared/ui/button";
import {Input} from "@/shared/ui/input";
import {useState} from "react";


interface AuthModalProps {
    isOpen: boolean;
    onClose?: () => void;
    isCodeSend?: boolean;
    sendCode?: (email: string) => void;
    verify?: (email: string, code: string) => void;
}

export const AuthModal = (
    {
        isOpen,
        onClose,
        isCodeSend,
        sendCode,
        verify,
    }: AuthModalProps
) => {
    const [email, setEmail] = useState("");
    const [code, setCode] = useState("");

    const handleSendCode = () => sendCode?.(email);
    const handleVerify = () => {
        verify?.(email, code);
        onClose?.();
    }

    return (
        <Modal isOpen={isOpen} onClose={onClose} className={styles.authModal}>
            <div className={styles.header}>
                <div className={styles.headerText}>
                    <h2 className={styles.title}>Войдите по почте</h2>
                    {!isCodeSend && (
                        <p className={styles.subtitle}>
                            Введите свой адрес электронной почты
                        </p>
                    )}
                </div>
            </div>

            <div className={styles.main}>
                {isCodeSend ? (
                    <>
                        <p className={styles.emailSent}>
                            Отправили код на почту {email}
                        </p>
                        <Input
                            className={styles.codeInput}
                            type="text"
                            placeholder="Код из письма"
                            value={code}
                            onChange={setCode}
                        />
                    </>
                ) : (
                    <Input
                        className={styles.emailInput}
                        type="email"
                        placeholder="Электронная почта"
                        value={email}
                        onChange={setEmail}
                    />
                )}
            </div>

            <div className={styles.footer}>
                {isCodeSend ? (
                    <>
                        <Button variant="primary" size="large" fullWidth onClick={handleVerify}>
                            Подтвердить
                        </Button>
                        <Button variant="secondaryGhost" size="small" onClick={handleSendCode}>
                            Отправить повторно
                        </Button>
                    </>
                ) : (
                    <>
                        <Button variant="primary" size="large" fullWidth onClick={handleSendCode}>
                            Отправить код
                        </Button>

                        <Button variant="secondaryGhost" size="small">
                            Не могу войти
                        </Button>
                    </>
                )}
            </div>
        </Modal>
    )
}