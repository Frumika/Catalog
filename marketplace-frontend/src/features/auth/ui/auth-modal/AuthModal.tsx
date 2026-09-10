import {Modal} from "@/shared/ui/modal";
import styles from "./AuthModal.module.css"
import {Button} from "@/shared/ui/button";
import {Input} from "@/shared/ui/input";
import {useState} from "react";
import {useSession} from "@/entities/session";
import {DemoButton} from "@/features/auth/ui/demo-button/DemoButton.tsx";
import {useNotify} from "@/shared/lib";


interface AuthModalProps {
    isOpen: boolean;
    onClose?: () => void;
}

export const AuthModal = (
    {
        isOpen,
        onClose,
    }: AuthModalProps
) => {
    const [email, setEmail] = useState("");
    const [code, setCode] = useState("");
    const {isCodeSend, sendCode, verify, demo} = useSession();
    const notify = useNotify();


    const handleSendCode = async () => {
        await sendCode(email);
    };

    const handleVerify = async () => {
        await verify(email, code);
        onClose?.();
    };

    const handleDemoEntrance = async () => {
        await demo(email);
        onClose?.();
    };


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
                        <DemoButton onClick={handleDemoEntrance}/>

                        <Button variant="primary" size="large" fullWidth onClick={handleSendCode}>
                            Отправить код
                        </Button>

                        <Button
                            variant="secondaryGhost"
                            size="small"
                            onClick={() => notify("warning", "Функционал восстановления и помощи в доступе пока не реализован")}
                        >
                            Не могу войти
                        </Button>
                    </>
                )}
            </div>
        </Modal>
    )
}